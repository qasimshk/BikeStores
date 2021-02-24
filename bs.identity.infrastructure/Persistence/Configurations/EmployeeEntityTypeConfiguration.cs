using bs.identity.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.identity.infrastructure.Persistence.Configurations
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.StoreId)
                .IsRequired();

            builder.Property(e => e.Email)
                .HasMaxLength(128)
                .IsRequired();

            builder.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(e => e.DateOfBirth)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(e => e.Designation)
                .IsRequired();
        }
    }
}
