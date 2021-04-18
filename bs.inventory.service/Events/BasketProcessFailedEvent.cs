using bs.component.integrations.Basket;
using System;

namespace bs.inventory.service.Events
{
    public class BasketProcessFailedEvent : IBasketProcessFailedEvent
    {
        public BasketProcessFailedEvent(Guid basketRef, string errorMessage)
        {
            BasketRef = basketRef;
            ErrorMessage = errorMessage;
        }

        public Guid BasketRef { get; }

        public string ErrorMessage { get; }
    }
}
