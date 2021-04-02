using bs.component.sharedkernal.Abstractions;
using bs.order.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bs.order.domain.Repositories
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        Task<IEnumerable<Payment>> FindByConditionAsync(Expression<Func<Payment, bool>> expression);

        void Add(Payment payment);

        void Update(Payment payment);
    }
}
