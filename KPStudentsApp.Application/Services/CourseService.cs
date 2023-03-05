using AutoMapper;
using KPStudentsApp.Application.Common;
using KPStudentsApp.Application.Common.Models;
using KPStudentsApp.Application.Interfaces;
using KPStudentsApp.Application.Models.RequestModels;
using KPStudentsApp.Application.Models.ResponseModels;
using KPStudentsApp.Domain.Entities;
using KPStudentsApp.Infrastructure.Interfaces;

namespace KPStudentsApp.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IMapper _mapper;

        public CourseService(
            IGenericRepository<Course> courseRepository,
            IMapper mapper)
        {
            _mapper = mapper;
            _courseRepository = courseRepository;
        }

        public async Task<Response<CourseResponseModel>> CreateCourse(CreateCourseModel model)
        {
            var course = _mapper.Map<Course>(model);

            if (!ValidateCourse(course, out string errorMessage))
                return Response<CourseResponseModel>.Fail(errorMessage);

            await _courseRepository.InsertAsync(course);

            var responseData = _mapper.Map<CourseResponseModel>(course);

            return Response<CourseResponseModel>.Success("Successfully Created Course", responseData);
        }

        public async Task<Response<CourseResponseModel>> UpdateCourse(UpdateCourseModel model)
        {
            var course = await _courseRepository.GetAsync(model.Id);

            if (course == null)
                return Response<CourseResponseModel>.Fail("Invalid Course Id");

            course = _mapper.Map(model, course);

            if (!ValidateCourse(course, out string errorMessage))
                return Response<CourseResponseModel>.Fail(errorMessage);

            await _courseRepository.UpdateAsync(course);

            var responseData = _mapper.Map<CourseResponseModel>(course);

            return Response<CourseResponseModel>.Success("Successfully Updated Course", responseData);
        }

        public async Task<Response<CourseResponseModel>> DeleteCourse(int courseId)
        {
            var course = await _courseRepository.GetAsync(courseId);

            if (course == null)
                return Response<CourseResponseModel>.Fail("Invalid Course Id");

            await _courseRepository.DeleteAsync(course);

            return Response<CourseResponseModel>.Success("Successfully Deleted Course");
        }

        public async Task<Response<CourseResponseModel>> GetCourse(int courseId)
        {
            var course = await _courseRepository.GetAsync(courseId);

            if (course == null)
                return Response<CourseResponseModel>.Fail("Invalid Course Id");

            var responseData = _mapper.Map<CourseResponseModel>(course);

            return Response<CourseResponseModel>.Success("Successfully Retrieved Course", responseData);
        }

        public SearchResponse<CourseResponseModel> SearchCourses(SearchRequest<string> searchRequest)
        {
            var courses = _courseRepository.TableNoTracking;

            if (!string.IsNullOrWhiteSpace(searchRequest.Data))
            {
                courses = courses.Where(x => x.Name.Contains(searchRequest.Data) || x.Code.Contains(searchRequest.Data));
            }

            courses = courses.OrderBy(x => x.CreatedAt).ThenBy(x => x.Name);

            return PaginationHelper.Paginate<Course, CourseResponseModel>
                (courses, _mapper, searchRequest.Page, searchRequest.PageSize, courses.Count());
        }

        #region Private Methods

        private bool ValidateCourse(Course course, out string errorMessage)
        {
            errorMessage = "";

            if (course == null)
            {
                errorMessage = "Invalid Course";
                return false;
            }

            if (_courseRepository.Table.Any(x => x.Name == course.Name && (course.Id <= 0 || x.Id != course.Id)))
            {
                errorMessage = $"A course with name {course.Name} already exists";
                return false;
            }

            if (_courseRepository.Table.Any(x => x.Code == course.Code && (course.Id <= 0 || x.Id != course.Id)))
            {
                errorMessage = $"A course with code {course.Code} already exists";
                return false;
            }

            return true;
        }

        #endregion

    }
}
