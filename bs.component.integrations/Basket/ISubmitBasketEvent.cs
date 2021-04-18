using System;

namespace bs.component.integrations.Basket
{
    public interface ISubmitBasketEvent
    {
        public Guid BasketRef { get; }
        public int ProductId { get; }
        public int Quantity { get; }
        public double ProductPrice { get; }
    }
}
