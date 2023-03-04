using AutoMapper;
using KPStudentsApp.Application.Interfaces;
using KPStudentsApp.Domain.Entities;
using KPStudentsApp.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace KPStudentsApp.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IGenericRepository<Student> _studentRepository;
        private readonly IGenericRepository<Course> _courseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<StudentService> _logger;

        public StudentService(
            IGenericRepository<Student> studentRepository,
            IGenericRepository<Course> courseRepository,
            IMapper mapper,
            ILogger<StudentService> logger)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _logger = logger;
            _courseRepository = courseRepository;
        }
    }
}
