using bs.order.domain.Entities;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bs.order.infrastructure.Persistence.Configurations
{
    public class OrderStateEntityTypeConfiguration : SagaClassMap<OrderState>
    {
        protected override void Configure(EntityTypeBuilder<OrderState> builder, ModelBuilder model)
        {
            builder.ToTable(nameof(OrderState));

            builder.Property(x => x.CurrentState).HasMaxLength(64);

            builder.Property(x => x.CreatedOn);

            builder.Property(x => x.FailedOn);
            
            builder.Property(x => x.TransactionRef);
        }
    }
}
