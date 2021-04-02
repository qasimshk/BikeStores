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
    public class PaymentRepository : IPaymentRepository
    {
        private readonly OrderDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public PaymentRepository(OrderDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Payment payment) => _context.Payments.Add(payment);

        public void Update(Payment payment) => _context.Payments.Update(payment);

        public async Task<IEnumerable<Payment>> FindByConditionAsync(Expression<Func<Payment, bool>> expression)
        {
            return await _context.Payments.Where(expression).ToListAsync();
        }
    }
}
