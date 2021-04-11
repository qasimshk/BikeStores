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
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ProductRepository(InventoryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Product>> FindByConditionAsync(Expression<Func<Product, bool>> expression)
        {
            return await _context.Products
                .Include(p => p.Stocks)
                .Where(expression).ToListAsync();
        }
    }
}
