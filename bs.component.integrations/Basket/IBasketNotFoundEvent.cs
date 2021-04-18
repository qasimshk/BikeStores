using System;

namespace bs.component.integrations.Basket
{
    public interface IBasketNotFoundEvent
    {
        public Guid BasketRef { get; }
        public Guid OrderRef { get; }
    }
}
