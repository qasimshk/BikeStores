using bs.order.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.order.infrastructure.Persistence.Configurations
{
    public class CustomerEntityTypeConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable(nameof(Customer));

            builder.HasKey(c => c.Id);

            builder.Property(c => c.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(11)
                .IsRequired();

            builder.Property(c => c.EmailAddress)
                .HasMaxLength(30)
                .IsRequired();

            builder.HasIndex(c => c.EmailAddress).IsUnique();

            builder.Property(c => c.Dob)
                .HasColumnType("datetime")
                .IsRequired();

            builder.OwnsOne(c => c.BillingAddress, a =>
            {
                a.Property<int>("CustomerId");
                a.WithOwner();
            });

            builder.Ignore(c => c.DomainEvents);
        }
    }
}
