using bs.order.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.order.infrastructure.Persistence.Configurations
{
    public class PaymentEntityTypeConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable(nameof(Payment));

            builder.HasKey(o => o.Id);

            builder.Property(p => p.PaymentRef).IsRequired();

            builder.Property(p => p.Amount).IsRequired();

            builder.Property(p => p.PaymentType).IsRequired();

            builder.Property(p => p.Status).IsRequired();

            builder.HasOne(p => p.Customer)
                .WithMany(c => c.Payments)
                .HasForeignKey("_customerId")
                .HasPrincipalKey(c => c.Id);

            builder.HasOne(p => p.CardDetail)
                .WithMany(p => p.Payments)
                .HasForeignKey("_cardDetailId")
                .HasPrincipalKey(cd => cd.Id);

        }
    }
}
