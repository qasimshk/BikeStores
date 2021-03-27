using bs.component.core.Extensions;
using bs.component.sharedkernal.Abstractions;
using bs.order.domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace bs.order.infrastructure.Persistence.Context
{
    public class OrderDbContext : DbContext, IUnitOfWork
    {
        
        private readonly IMediator _mediator;

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CardDetail> CardDetails { get; set; }
        public DbSet<Consent> Consents { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _mediator.DispatchDomainEventsAsync(this);
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
