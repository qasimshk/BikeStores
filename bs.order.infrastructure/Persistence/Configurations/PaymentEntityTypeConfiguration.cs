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

            builder.Property(p => p.Amount).HasColumnType("decimal(5,2)").IsRequired();

            builder.Property(p => p.PaymentType).IsRequired();

            builder.Property(p => p.Status).IsRequired();

            builder.Property<int>("_customerId")
                .HasColumnName("CustomerId")
                .IsRequired();

            builder.Property<int?>("_cardDetailId")
                .HasColumnName("CardDetailId");

            builder.HasOne(p => p.Customer)
                .WithMany(c => c.Payments)
                .HasForeignKey("_customerId")
                .HasPrincipalKey(c => c.Id)
                .IsRequired();

            builder.HasOne(p => p.CardDetail)
                .WithMany(p => p.Payments)
                .HasForeignKey("_cardDetailId")
                .HasPrincipalKey(cd => cd.Id);

            builder.HasOne(o => o.Order)
                .WithOne(p => p.Payment)
                .HasForeignKey<Order>("_paymentId")
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired();

            builder.Ignore(c => c.DomainEvents);
        }
    }
}
