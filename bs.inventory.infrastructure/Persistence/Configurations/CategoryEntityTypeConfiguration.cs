using bs.inventory.domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.inventory.infrastructure.Persistence.Configurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.Ignore(c => c.DomainEvents);

            builder.HasMany(c => c.Products)
                .WithOne(c => c.Category)
                .HasForeignKey("_categoryId")
                .IsRequired();
        }
    }
}
