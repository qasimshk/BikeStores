using bs.component.integrations.Basket;
using System;

namespace bs.order.service.Events
{
    public class BasketStatusRequestEvent : IBasketStatusRequestEvent
    {
        public Guid BasketRef { get; set; }
    }
}
