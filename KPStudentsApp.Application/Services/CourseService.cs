using AutoMapper;
using KPStudentsApp.Application.Interfaces;
using KPStudentsApp.Domain.Entities;
using KPStudentsApp.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace KPStudentsApp.Application.Services
{
    public class CourseService : ICourseService
    {
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CourseService> _logger;

        public CourseService(
            IGenericRepository<Course> courseRepository,
            IMapper mapper,
            ILogger<CourseService> logger)
        {
            _mapper = mapper;
            _logger = logger;
            _courseRepository = courseRepository;
        }
    }
}
