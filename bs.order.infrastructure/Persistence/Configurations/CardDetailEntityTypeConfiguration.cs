﻿using bs.order.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.order.infrastructure.Persistence.Configurations
{
    public class CardDetailEntityTypeConfiguration : IEntityTypeConfiguration<CardDetail>
    {
        public void Configure(EntityTypeBuilder<CardDetail> builder)
        {
            builder.ToTable(nameof(CardDetail));

            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.CardHolderName).IsRequired();

            builder.Property(c => c.CardType).IsRequired();

            builder.Property<int>("_customerId")
                .HasColumnName("CustomerId")
                .IsRequired();
            
            builder.Ignore(c => c.DomainEvents);
        }
    }
}