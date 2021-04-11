using bs.component.sharedkernal.Abstractions;
using bs.inventory.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bs.inventory.domain.Respositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<Category>> FindByConditionAsync(Expression<Func<Category, bool>> expression);

        void Add(Category category);
    }
}
