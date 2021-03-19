using bs.order.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.order.infrastructure.Persistence.Configurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable(nameof(Order));

            builder.HasKey(c => c.Id);

            builder.Property(o => o.OrderRef).IsRequired();

            builder.Property(o => o.Status).IsRequired();

            builder.OwnsOne(o => o.DeliveryAddress, a =>
            {
                a.Property<int>("OrderId");
                a.WithOwner();
            });

            builder.HasOne(p => p.Payment)
                .WithOne(o => o.Order)
                .HasForeignKey<Order>("_paymentId")
                .HasPrincipalKey<Payment>(p => p.Id);

            builder.HasOne(c => c.Customer)
                .WithMany(o => o.Orders)
                .HasForeignKey("_customerId")
                .HasPrincipalKey(c => c.Id);
        }
    }
}
