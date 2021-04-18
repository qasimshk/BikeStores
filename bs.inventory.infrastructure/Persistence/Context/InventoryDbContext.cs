using bs.component.core.Extensions;
using bs.component.sharedkernal.Abstractions;
using bs.inventory.infrastructure.Persistence.Configurations;
using MassTransit.EntityFrameworkCoreIntegration;
using MassTransit.EntityFrameworkCoreIntegration.Mappings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using bs.inventory.domain.Entities;

namespace bs.inventory.infrastructure.Persistence.Context
{
    public class InventoryDbContext : SagaDbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(InventoryDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        protected override IEnumerable<ISagaClassMap> Configurations
        {
            get { yield return new InventoryStateEntityTypeConfiguration(); }
        }
        
        public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
