using System;

namespace bs.component.integrations.Basket
{
    public interface IBasketStatusEvent
    {
        public Guid BasketRef { get; }
        public Guid OrderRef { get; }
        public string CurrentState { get; }
        public double BasketPrice { get; }
        public string JsonBasketItems { get; }
    }
}
