using KPStudentsApp.Application.Common.Models;
using KPStudentsApp.Application.Models.RequestModels;
using KPStudentsApp.Application.Models.ResponseModels;

namespace KPStudentsApp.Application.Interfaces
{
    public interface IStudentService
    {
        Task<Response<StudentResponseModel>> CreateStudent(CreateStudentModel model);
        Task<Response<string>> DeleteStudent(int stuedentId);
        Task<Response<StudentResponseModel>> GetStudent(int studentId);
        Task<SearchResponse<StudentResponseModelSlim>> SearchStudents(SearchRequest<string> searchRequest);
        Task<Response<StudentResponseModel>> UpdateStudent(UpdateStudentModel model);
    }
}
