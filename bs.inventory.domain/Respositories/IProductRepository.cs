using bs.component.sharedkernal.Abstractions;
using bs.inventory.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bs.inventory.domain.Respositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<Product>> FindByConditionAsync(Expression<Func<Product, bool>> expression);
    }
}
