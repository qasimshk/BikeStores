using bs.component.integrations.Basket;
using System;

namespace bs.inventory.service.Events
{
    public class BasketValidateEvent : IBasketValidateEvent
    {
        public BasketValidateEvent(Guid basketRef)
        {
            BasketRef = basketRef;
        }

        public Guid BasketRef { get; }
    }
}
