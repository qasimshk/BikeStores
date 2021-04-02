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
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public OrderRepository(OrderDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Order order) => _context.Orders.Add(order);

        public void Update(Order order) => _context.Orders.Update(order);
        
        public async Task<IEnumerable<Order>> FindByConditionAsync(Expression<Func<Order, bool>> expression)
        {
            return await _context.Orders.Where(expression).ToListAsync();
        }
    }
}
