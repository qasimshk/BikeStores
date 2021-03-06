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
                .HasColumnType("date")
                .HasColumnName("DateOfBirth")
                .IsRequired();

            builder.OwnsOne(c => c.BillingAddress, ba =>
            {
                ba.Property<int>("CustomerId");

                ba.Property(d => d.Street)
                    .HasMaxLength(100);

                ba.Property(d => d.PostCode)
                    .HasMaxLength(100);

                ba.Property(d => d.City)
                    .HasMaxLength(100);

                ba.Property(d => d.Country)
                    .HasMaxLength(100);
            });

            builder.Ignore(c => c.DomainEvents);

            builder.HasOne(c => c.Consents)
                .WithOne(c => c.Customer)
                .HasForeignKey<Consent>("_customerId")
                .OnDelete(DeleteBehavior.Restrict)
                .HasPrincipalKey<Customer>(c => c.Id)
                .IsRequired();

            builder.HasMany(cd => cd.CardDetails)
                .WithOne(c => c.Customer)
                .HasForeignKey("_customerId")
                .HasPrincipalKey(c => c.Id);
        }
    }
}
