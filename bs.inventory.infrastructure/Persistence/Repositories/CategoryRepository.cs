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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InventoryDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public CategoryRepository(InventoryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Category category) => _context.Categories.Add(category);
        
        public async Task<List<Category>> FindByConditionAsync(Expression<Func<Category, bool>> expression)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .Where(expression).ToListAsync();
        }
    }
}
