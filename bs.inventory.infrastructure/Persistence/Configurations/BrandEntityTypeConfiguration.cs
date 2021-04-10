using bs.inventory.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.inventory.infrastructure.Persistence.Configurations
{
    public class BrandEntityTypeConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable(nameof(Brand));

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            builder.Ignore(b => b.DomainEvents);

            builder.HasMany(b => b.Products)
                .WithOne(b => b.Brand)
                .HasForeignKey("_brandId")
                .IsRequired();
        }
    }
}
