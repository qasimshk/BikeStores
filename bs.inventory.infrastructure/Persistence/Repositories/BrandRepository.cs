using bs.component.sharedkernal.Abstractions;
using bs.inventory.domain.Entities;
using bs.inventory.domain.Respositories;
using bs.inventory.infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bs.inventory.infrastructure.Persistence.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly InventoryDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public BrandRepository(InventoryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Brand brand) => _context.Brands.Add(brand);

        public async Task<List<Brand>> FindByConditionAsync(Expression<Func<Brand, bool>> expression)
        {
            return await _context.Brands
                .Include(b => b.Products)
                .Where(expression).ToListAsync();
        }
    }
}
