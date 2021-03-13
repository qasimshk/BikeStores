using bs.component.sharedkernal.Abstractions;
using bs.order.domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using bs.order.infrastructure.Persistence.Configurations;

namespace bs.order.infrastructure.Persistence.Context
{
    public class OrderDbContext : DbContext, IUnitOfWork
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }
        
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CardDetail> CardDetails { get; set; }
        public DbSet<Consent> Consents { get; set; }
        //public DbSet<Order> Orders { get; set; }
        //public DbSet<OrderItem> OrderItems { get; set; }
        //public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ConsentEntityTypeConfiguration());

            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderDbContext).Assembly);
        }

        public Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
