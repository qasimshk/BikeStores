using bs.component.sharedkernal.Abstractions;
using bs.order.domain.Entities;
using bs.order.domain.Repositories;
using bs.order.infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bs.order.infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrderDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public CustomerRepository(OrderDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Customer customer) => _context.Customers.Add(customer);
        
        public void Update(Customer customer) => _context.Customers.Update(customer);

        public async Task<IEnumerable<Customer>> FindByConditionAsync(Expression<Func<Customer, bool>> expression)
        {
            return await _context.Customers.Where(expression).ToListAsync();
        }
    }
}
