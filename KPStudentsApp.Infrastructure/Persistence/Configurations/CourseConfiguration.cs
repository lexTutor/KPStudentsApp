﻿using KPStudentsApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KPStudentsApp.Infrastructure.Persistence.Configurations
{
    internal class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id).IsClustered();
            builder.Property(x => x.Id).UseIdentityColumn(seed: 1, increment: 1);
            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.CreditUnit).IsRequired();
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
