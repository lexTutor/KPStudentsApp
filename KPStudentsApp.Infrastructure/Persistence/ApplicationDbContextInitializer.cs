using KPStudentsApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace KPStudentsApp.Infrastructure.Persistence
{
    public class ApplicationDbContextInitialiser
    {
        private readonly ILogger<ApplicationDbContextInitialiser> _logger;
        private readonly ApplicationDbContext _context;

        public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Default data
            // Seed, if necessary
            if (!_context.Course.Any())
            {
                _context.Course.Add(new Course
                {
                    Code = "MTH501",
                    Name = "Numerical Anaylsis",
                    CreditUnit = 4,
                    Students = new List<Student>
                        {
                            new Student
                            {
                                 Email = "JesusJoseph@qa.team",
                                 FirstName = "Jesus",
                                 LastName = "Joseph",
                                 RegisteredOn = DateTime.UtcNow,
                                 RegisterationNumber = "SP-001"
                            },
                            new Student
                            {
                                Email = "DavidJesse@qa.team",
                                 FirstName = "David",
                                 LastName = "Jesse",
                                 RegisteredOn = DateTime.UtcNow,
                                 RegisterationNumber = "SP-002"
                            }
                        }
                });

                await _context.SaveChangesAsync();
            }
        }
    }
}