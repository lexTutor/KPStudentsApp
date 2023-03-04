using KPStudentsApp.Domain.Entities;
using KPStudentsApp.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KPStudentsApp.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        private readonly EntitySaveChangesInterceptor _entitySaveChangesInterceptor;

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options,
            EntitySaveChangesInterceptor entitySaveChangesInterceptor)
            : base(options)
        {
            _entitySaveChangesInterceptor = entitySaveChangesInterceptor;
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<ReferenceNumber> ReferenceNumber { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            foreach (var property in builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }

            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_entitySaveChangesInterceptor);
        }
    }
}
