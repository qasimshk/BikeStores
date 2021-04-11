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
    public class BasketRepository : IBasketRepository
    {
        private readonly InventoryDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public BasketRepository(InventoryDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Add(Basket basket) => _context.Add(basket);

        public void Update(Basket basket) => _context.Update(basket);

        public void Delete(int basketId)
        {
            var basket = _context.Baskets.Single(b => b.Id == basketId);
            _context.Baskets.Remove(basket);
        }

        public async Task<List<Basket>> FindByConditionAsync(Expression<Func<Basket, bool>> expression)
        {
            return await _context.Baskets
                .Include(b => b.BasketItems)
                .Where(expression).ToListAsync();
        }

    }
}
