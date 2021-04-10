using bs.inventory.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.inventory.infrastructure.Persistence.Configurations
{
    public class StoreEntityTypeConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.ToTable(nameof(Store));

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .HasMaxLength(30)
                .IsRequired();

            builder.Property(s => s.Phone)
                .HasMaxLength(30)
                .IsRequired();

            builder.OwnsOne(c => c.StoreAddress, sa =>
            {
                sa.Property<int>("StoreId");

                sa.Property(d => d.Street)
                    .HasMaxLength(100);

                sa.Property(d => d.PostCode)
                    .HasMaxLength(100);

                sa.Property(d => d.City)
                    .HasMaxLength(100);

                sa.Property(d => d.Country)
                    .HasMaxLength(100);
            });
        }
    }
}
