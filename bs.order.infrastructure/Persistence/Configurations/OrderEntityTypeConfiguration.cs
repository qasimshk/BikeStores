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

            builder.HasKey(o => o.Id);

            builder.Property(o => o.OrderRef)
                .IsRequired();

            builder.Property(o => o.Status)
                .IsRequired();

            builder.Property<int>("_paymentId")
                .HasColumnName("PaymentId")
                .IsRequired();

            builder.Property<int>("_customerId")
                .HasColumnName("CustomerId")
                .IsRequired();

            builder.OwnsOne(o => o.DeliveryAddress, da =>
            {
                da.Property<int>("OrderId");

                da.Property(d => d.Street)
                    .HasMaxLength(100);

                da.Property(d => d.PostCode)
                    .HasMaxLength(100);

                da.Property(d => d.City)
                    .HasMaxLength(100);

                da.Property(d => d.Country)
                    .HasMaxLength(100);
            });
            
            builder.HasOne(c => c.Customer)
                .WithMany(o => o.Orders)
                .HasForeignKey("_customerId")
                .OnDelete(DeleteBehavior.Restrict)
                .HasPrincipalKey(c => c.Id)
                .IsRequired();

            builder.HasMany(oi => oi.OrderItems)
                .WithOne(o => o.Order)
                .HasForeignKey("_orderId")
                .OnDelete(DeleteBehavior.Restrict)
                .HasPrincipalKey(o => o.Id)
                .IsRequired();
            
            builder.Ignore(c => c.DomainEvents);
        }
    }
}
