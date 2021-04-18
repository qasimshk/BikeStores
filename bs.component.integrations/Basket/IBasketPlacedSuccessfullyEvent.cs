using System;

namespace bs.component.integrations.Basket
{
    public interface IBasketPlacedSuccessfullyEvent
    {
        public Guid BasketRef { get; }
        public double BasketPrice { get; }
        public string JsonBasketItems { get; }
    }
}
