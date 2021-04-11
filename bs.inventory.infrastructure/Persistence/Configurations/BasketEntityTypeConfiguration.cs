using bs.inventory.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.inventory.infrastructure.Persistence.Configurations
{
    public class BasketEntityTypeConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.ToTable(nameof(Basket));

            builder.HasKey(b => b.Id);

            builder.Property(b => b.BasketRef)
                .IsRequired();
            
            builder.HasMany(b => b.BasketItems)
                .WithOne(b => b.Basket)
                .HasForeignKey("_basketId")
                .OnDelete(DeleteBehavior.Restrict)
                .HasPrincipalKey(b => b.Id)
                .IsRequired();
        }
    }
}
