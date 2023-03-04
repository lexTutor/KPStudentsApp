using KPStudentsApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KPStudentsApp.Infrastructure.Persistence.Configurations
{
    internal class ReferenceNumberConfiguration : IEntityTypeConfiguration<ReferenceNumber>
    {
        public void Configure(EntityTypeBuilder<ReferenceNumber> builder)
        {
            builder.HasKey(x => x.Id).IsClustered();
            builder.Property(x => x.Id).UseIdentityColumn(seed: 1, increment: 1);
        }
    }
}
