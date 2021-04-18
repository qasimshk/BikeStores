using bs.component.integrations.Basket;
using System;

namespace bs.order.service.Events
{
    public class SubmitBasketValidateEvent : ISubmitBasketValidateEvent
    {
        public Guid BasketRef { get; set; }
        public Guid OrderRef { get; set; }
    }
}
