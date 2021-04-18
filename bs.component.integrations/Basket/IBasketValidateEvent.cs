using System;

namespace bs.component.integrations.Basket
{
    public interface IBasketValidateEvent
    {
        public Guid BasketRef { get; }
    }
}
