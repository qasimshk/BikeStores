using bs.component.sharedkernal.Abstractions;
using bs.component.sharedkernal.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace bs.inventory.domain.Entities
{
    public class Basket : Entity, IAggregateRoot
    {
        private List<BasketItem> _basketItems;

        public Basket()
        {
            _basketItems = new List<BasketItem>();
        }

        public Basket(Guid basketRef, int productId, int quantity, double amount)
        {
            BasketRef = basketRef;

            _basketItems = new List<BasketItem>
            {
                new(productId,quantity,amount, Id)
            };
        }

        public Guid BasketRef { get; private set; }

        public IList<BasketItem> BasketItems => _basketItems;

        public double Total => _basketItems.Select(b => b.Amount).Sum();

        public void AddBasketItem(int productId, int quantity, double amount)
        {
            _basketItems = new List<BasketItem>
            {
                new(productId,quantity,amount, Id)
            };
        }
    }
}
