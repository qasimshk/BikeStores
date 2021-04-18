using bs.component.integrations.Basket;
using System;

namespace bs.inventory.service.Events
{
    public class BasketItemsStockCheckResultEvent : IBasketItemsStockCheckResultEvent
    {
        public Guid BasketRef { get; set; }
        public bool InStock { get; set; }
    }
}
