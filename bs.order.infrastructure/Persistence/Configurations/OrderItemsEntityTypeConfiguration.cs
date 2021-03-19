﻿using bs.order.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.order.infrastructure.Persistence.Configurations
{
    public class OrderItemsEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable(nameof(OrderItem));

            builder.HasKey(o => o.Id);

            builder.Property(o => o.ProductName).IsRequired();

            builder.Property(o => o.ProductRef).IsRequired();

            builder.Property(o => o.Quantity).IsRequired();

            builder.Property(o => o.IndividualPrice).IsRequired();

            builder.HasOne(o => o.Order)
                .WithMany(oi => oi.OrderItems)
                .HasForeignKey("_orderId")
                .HasPrincipalKey(o => o.Id);
        }
    }
}
