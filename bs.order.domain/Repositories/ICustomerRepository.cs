using bs.component.sharedkernal.Abstractions;
using bs.order.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bs.order.domain.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<IEnumerable<Customer>> FindByConditionAsync(Expression<Func<Customer, bool>> expression);

        void Add(Customer customer);

        void Update(Customer customer);
    }
}
