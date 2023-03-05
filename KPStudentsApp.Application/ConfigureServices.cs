using FluentValidation;
using KPStudentsApp.Application.Common.Mapping;
using KPStudentsApp.Application.Interfaces;
using KPStudentsApp.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace KPStudentsApp.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperInitializer));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IReferenceNumberService, ReferenceNumberService>();
            return services;
        }
    }
}
