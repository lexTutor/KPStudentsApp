using KPStudentsApp.Application.Common.Models;
using KPStudentsApp.Application.Models.RequestModels;
using KPStudentsApp.Application.Models.ResponseModels;

namespace KPStudentsApp.Application.Interfaces
{
    public interface ICourseService
    {
        Task<Response<CourseResponseModel>> CreateCourse(CreateCourseModel model);
        Task<Response<CourseResponseModel>> DeleteCourse(int courseId);
        Task<Response<CourseResponseModel>> GetCourse(int courseId);
        SearchResponse<CourseResponseModel> SearchCourses(SearchRequest<string> searchRequest);
        Task<Response<CourseResponseModel>> UpdateCourse(UpdateCourseModel model);
    }
}
