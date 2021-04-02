using bs.component.sharedkernal.Abstractions;
using bs.order.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bs.order.domain.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> FindByConditionAsync(Expression<Func<Order, bool>> expression);

        void Add(Order order);

        void Update(Order order);
    }
}
