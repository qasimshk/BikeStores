using bs.component.sharedkernal.Abstractions;
using bs.inventory.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bs.inventory.domain.Respositories
{
    public interface IBasketRepository : IRepository<Basket>
    {
        Task<List<Basket>> FindByConditionAsync(Expression<Func<Basket, bool>> expression);
        void Add(Basket basket);
        void Update(Basket basket);
        void Delete(int basketId);
    }
}
