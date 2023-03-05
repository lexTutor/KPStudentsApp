using AutoMapper;
using KPStudentsApp.Application.Models.RequestModels;
using KPStudentsApp.Application.Models.ResponseModels;
using KPStudentsApp.Domain.Entities;

namespace KPStudentsApp.Application.Common.Mapping
{
    public class AutoMapperInitializer : Profile
    {
        public AutoMapperInitializer()
        {
            CreateMap<UpdateCourseModel, Course>();
            CreateMap<CreateCourseModel, Course>();
            CreateMap<Course, CourseResponseModel>();
        }
    }
}
