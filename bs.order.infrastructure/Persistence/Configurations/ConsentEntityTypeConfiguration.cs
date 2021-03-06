using bs.order.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.order.infrastructure.Persistence.Configurations
{
    public class ConsentEntityTypeConfiguration : IEntityTypeConfiguration<Consent>
    {
        public void Configure(EntityTypeBuilder<Consent> builder)
        {
            builder.ToTable(nameof(Consent));

            builder.HasKey(c => c.Id);

            builder.Property(c => c.ContactByEmail)
                .IsRequired();

            builder.Property(c => c.ContactByText)
                .IsRequired();

            builder.Property(c => c.ContactByCall)
                .IsRequired();

            builder.Property(c => c.ContactByPost)
                .IsRequired();

            builder.Property<int>("_customerId")
                .HasColumnName("CustomerId")
                .IsRequired();
            
            builder.Ignore(c => c.DomainEvents);
        }
    }
}
