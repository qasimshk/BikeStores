using bs.component.integrations.Basket;
using System;

namespace bs.order.service.Events
{
    public class BasketStatusRequestEvent : IBasketStatusRequestEvent
    {
        public BasketStatusRequestEvent(Guid basketRef)
        {
            BasketRef = basketRef;
        }

        public Guid BasketRef { get; private set; }
    }
}
