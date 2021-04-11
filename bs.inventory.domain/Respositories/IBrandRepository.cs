using bs.component.sharedkernal.Abstractions;
using bs.inventory.domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bs.inventory.domain.Respositories
{
    public interface IBrandRepository : IRepository<Brand>
    {
        Task<List<Brand>> FindByConditionAsync(Expression<Func<Brand, bool>> expression);

        void Add(Brand brand);
    }
}
