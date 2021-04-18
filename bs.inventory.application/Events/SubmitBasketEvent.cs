using bs.component.integrations.Basket;
using System;

namespace bs.inventory.application.Events
{
    public class SubmitBasketEvent : ISubmitBasketEvent
    {
        public Guid BasketRef { get; init; }
        public int ProductId { get; init; }
        public int Quantity { get; init; }
        public double ProductPrice { get; init; }
    }
}
