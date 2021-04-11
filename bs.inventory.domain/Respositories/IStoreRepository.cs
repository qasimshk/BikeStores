using bs.component.sharedkernal.Abstractions;
using bs.inventory.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bs.inventory.domain.Respositories
{
    public interface IStoreRepository : IRepository<Store>
    {
        Task<List<Store>> FindByConditionAsync(Expression<Func<Store, bool>> expression);
        void Add(Store store);
    }
}
