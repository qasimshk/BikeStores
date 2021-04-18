using System;

namespace bs.component.integrations.Basket
{
    public interface ISubmitBasketValidateEvent
    {
        public Guid BasketRef { get; }
        public Guid OrderRef { get; }
    }
}
