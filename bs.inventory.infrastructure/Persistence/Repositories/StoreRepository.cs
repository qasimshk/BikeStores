using bs.component.sharedkernal.Abstractions;
using bs.inventory.domain.Entities;
using bs.inventory.domain.Respositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using bs.inventory.infrastructure.Persistence.Context;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace bs.inventory.infrastructure.Persistence.Repositories
{
    public class StoreRepository : IStoreRepository
    {
        private readonly InventoryDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public StoreRepository(InventoryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Store store) => _context.Stores.Add(store);

        public async Task<List<Store>> FindByConditionAsync(Expression<Func<Store, bool>> expression)
        {
            return await _context.Stores.Where(expression).ToListAsync();
        }
    }
}
