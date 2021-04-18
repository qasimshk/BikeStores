using bs.inventory.domain.Entities;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.inventory.infrastructure.Persistence.Configurations
{
    public class InventoryStateEntityTypeConfiguration : SagaClassMap<InventoryStatus>
    {
        protected override void Configure(EntityTypeBuilder<InventoryStatus> builder, ModelBuilder model)
        {
            builder.ToTable(nameof(InventoryStatus));

            builder.Property(x => x.CurrentState).HasMaxLength(64);

            builder.Property(x => x.CreatedOn);

            builder.Property(x => x.FailedOn);

            builder.Property(x => x.BasketRef);

            builder.Property(x => x.JsonBasketItems);

            builder.Property(x => x.BasketPrice);

            builder.Property(x => x.BasketRef).IsRequired();
        }
    }
}
