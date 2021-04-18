using bs.component.integrations.Basket;
using bs.component.integrations.Requests;
using System;

namespace bs.order.service.Events
{
    public class SubmitBasketValidateEvent : ISubmitBasketValidateEvent
    {
        private readonly IOrderSubmitEvent _orderSubmit;

        public SubmitBasketValidateEvent(IOrderSubmitEvent orderSubmit)
        {
            _orderSubmit = orderSubmit;
        }

        public Guid BasketRef => _orderSubmit.BasketRef;
        public Guid OrderRef => _orderSubmit.OrderRef;
    }
}
