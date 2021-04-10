using bs.inventory.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.inventory.infrastructure.Persistence.Configurations
{
    public class StockEntityTypeConfiguration : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable(nameof(Stock));

            builder.HasKey(s => s.Id);

            builder.Property<int>("_storeId")
                .HasColumnName("StoreId")
                .IsRequired();

            builder.Property<int>("_productId")
                .HasColumnName("ProductId")
                .IsRequired();

            builder.Property<int>("_stockIn")
                .HasColumnName("StockIn")
                .IsRequired();

            builder.Property<int>("_stockOut")
                .HasColumnName("StockOut")
                .IsRequired();

            builder.Ignore(c => c.DomainEvents);
        }
    }
}
