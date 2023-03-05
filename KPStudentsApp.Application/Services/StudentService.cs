using AutoMapper;
using KPStudentsApp.Application.Common;
using KPStudentsApp.Application.Common.Models;
using KPStudentsApp.Application.Interfaces;
using KPStudentsApp.Application.Models.RequestModels;
using KPStudentsApp.Application.Models.ResponseModels;
using KPStudentsApp.Domain.Entities;
using KPStudentsApp.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KPStudentsApp.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IReferenceNumberService _referenceNumberService;
        private readonly IMapper _mapper;

        public StudentService(
            IGenericRepository<Student> studentRepository,
            IGenericRepository<Course> courseRepository,
            IMapper mapper,
            IReferenceNumberService referenceNumberService)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _courseRepository = courseRepository;
            _referenceNumberService = referenceNumberService;
        }

        public async Task<Response<StudentResponseModel>> CreateStudent(CreateStudentModel model)
        {
            if (!ValidateCourseIds(model.CourseIds, out string courseErrorMessage, out var matchingCourses))
                return Response<StudentResponseModel>.Fail(courseErrorMessage);

            var student = _mapper.Map<Student>(model);

            if (!ValidateStudent(student, out string errorMessage))
                return Response<StudentResponseModel>.Fail(errorMessage);

            student.Courses = matchingCourses;
            student.RegisterationNumber = await ReferenceNumberServiceExtension.GetReferenceNumber(_referenceNumberService, $"REG", "000000");

            await _studentRepository.InsertAsync(student);

            var responseData = _mapper.Map<StudentResponseModel>(student);

            return Response<StudentResponseModel>.Success("Successfully Created Student", responseData);
        }

        public async Task<Response<StudentResponseModel>> UpdateStudent(UpdateStudentModel model)
        {
            var student = await _studentRepository.Table.Where(x => x.Id == model.Id)
                .Include(x=>x.Courses).FirstOrDefaultAsync();

            if (student == null)
                return Response<StudentResponseModel>.Fail("Invalid Student Id");

            if (!ValidateCourseIds(model.CourseIds, out string courseErrorMessage, out var matchingCourses))
                return Response<StudentResponseModel>.Fail(courseErrorMessage);

            student = _mapper.Map(model, student);
            student.Courses = matchingCourses;

            if (!ValidateStudent(student, out string errorMessage))
                return Response<StudentResponseModel>.Fail(errorMessage);

            await _studentRepository.UpdateAsync(student);

            var responseData = _mapper.Map<StudentResponseModel>(student);

            return Response<StudentResponseModel>.Success("Successfully Updated Student Information", responseData);
        }

        public async Task<Response<string>> DeleteStudent(int stuedentId)
        {
            var student = await _studentRepository.GetAsync(stuedentId);

            if (student == null)
                return Response<string>.Fail("Invalid Student Id");

            await _studentRepository.DeleteAsync(student);

            return Response<string>.Success("Successfully Deleted Student");
        }

        public async Task<Response<StudentResponseModel>> GetStudent(int studentId)
        {
            var student = await _studentRepository.TableNoTracking.Where(x => x.Id == studentId)
                .Include(x => x.Courses).FirstOrDefaultAsync();

            if (student == null)
                return Response<StudentResponseModel>.Fail("Invalid Student Id");

            var responseData = _mapper.Map<StudentResponseModel>(student);

            return Response<StudentResponseModel>.Success("Successfully Retrieved Student", responseData);
        }

        public async Task<SearchResponse<StudentResponseModelSlim>> SearchStudents(SearchRequest<string> searchRequest)
        {
            var students = _studentRepository.TableNoTracking;

            if (!string.IsNullOrWhiteSpace(searchRequest.Data))
            {
                students = students.Where(x => x.FirstName.Contains(searchRequest.Data)
                || x.LastName.Contains(searchRequest.Data) || x.Email.Contains(searchRequest.Data)
                || x.RegisterationNumber.Contains(searchRequest.Data) || x.PhoneNumber.Contains(searchRequest.Data));
            }

            var totalCount = students.Count();

            var query = await (from student in students
                               select new StudentResponseModelSlim
                               {
                                   Id = student.Id,
                                   RegisterationNumber = student.RegisterationNumber,
                                   Email = student.Email,
                                   FirstName = student.FirstName,
                                   LastName = student.LastName,
                                   PhoneNumber = student.PhoneNumber,
                                   RegisteredOn = student.RegisteredOn
                               }).OrderBy(x => x.RegisterationNumber)
                                 .ThenBy(x => x.RegisteredOn)
                                 .ToListAsync();

            var skipTake = PaginationHelper.SkipTake(searchRequest.PageSize, searchRequest.Page);

            query = query.Skip(skipTake.Item1).Take(skipTake.Item2).ToList();

            return PaginationHelper.Paginate
                (query, searchRequest.Page, searchRequest.PageSize, totalCount);
        }

        #region Private Methods

        private bool ValidateStudent(Student student, out string errorMessage)
        {
            errorMessage = "";

            if (student == null)
            {
                errorMessage = "Invalid Student";
                return false;
            }

            if (_studentRepository.Table.Any(x => x.Email == student.Email &&
                (student.Id <= 0 || x.Id != student.Id)))
            {
                errorMessage = $"A Student with Email {student.Email} already exists";
                return false;
            }

            return true;
        }

        private bool ValidateCourseIds(List<int> courseIds, out string errorMessage, out List<Course> matchingCourses)
        {
            errorMessage = "";
            matchingCourses = null;

            if (courseIds == null)
            {
                errorMessage = "Invalid Courses";
                return false;
            }

            matchingCourses = _courseRepository.Table.Where(c => courseIds.Contains(c.Id)).ToList();

            bool allCoursesDoNotExist = matchingCourses.Count != courseIds.Count;

            if (allCoursesDoNotExist)
            {
                errorMessage = $"One or More courses are invalid";
                return false;
            }

            return true;
        }

        #endregion
    }
}
