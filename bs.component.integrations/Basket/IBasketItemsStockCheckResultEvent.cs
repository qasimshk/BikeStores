using System;

namespace bs.component.integrations.Basket
{
    public interface IBasketItemsStockCheckResultEvent
    {
        public Guid BasketRef { get; set; }
        public bool InStock { get; set; }
    }
}
