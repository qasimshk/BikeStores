using bs.inventory.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.inventory.infrastructure.Persistence.Configurations
{
    public class BasketItemEntityTypeConfiguration : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.ToTable(nameof(BasketItem));

            builder.HasKey(b => b.Id);

            builder.Property<int>("_basketId")
                .HasColumnName("BasketId")
                .IsRequired();

            builder.Property<int>("_productId")
                .HasColumnName("ProductId")
                .IsRequired();
        }
    }
}
