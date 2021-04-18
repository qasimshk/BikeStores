using System;

namespace bs.component.integrations.Basket
{
    public interface IBasketProcessFailedEvent
    {
        public Guid BasketRef { get; }
        public string ErrorMessage { get; }
    }
}
