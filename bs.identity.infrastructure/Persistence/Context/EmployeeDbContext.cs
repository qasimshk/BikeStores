using bs.identity.domain.Entities;
using bs.identity.infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bs.identity.infrastructure.Persistence.Context
{
    public class EmployeeDbContext : IdentityDbContext<Employee>
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new EmployeeEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
