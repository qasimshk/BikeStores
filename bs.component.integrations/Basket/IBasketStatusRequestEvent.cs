using System;

namespace bs.component.integrations.Basket
{
    public interface IBasketStatusRequestEvent
    {
        public Guid BasketRef { get; }
    }
}
