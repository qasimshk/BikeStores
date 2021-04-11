using System;
using bs.inventory.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.inventory.infrastructure.Persistence.Configurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable(nameof(Product));

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(p => p.ModelYear)
                .IsRequired();

            builder.Property(p => p.ListPrice)
                .IsRequired();

            builder.Property<Guid>("ProductRef")
                .HasColumnName("Reference")
                .HasDefaultValueSql("NEWID()");

            builder.Property<int>("_brandId")
                .HasColumnName("BrandId")
                .IsRequired();

            builder.Property<int>("_categoryId")
                .HasColumnName("CategoryId")
                .IsRequired();

            builder.HasMany(p => p.Stocks)
                .WithOne(p => p.Product)
                .HasForeignKey("_productId")
                .OnDelete(DeleteBehavior.Restrict)
                .HasPrincipalKey(p => p.Id)
                .IsRequired();

            builder.Ignore(c => c.DomainEvents);
        }
    }
}
