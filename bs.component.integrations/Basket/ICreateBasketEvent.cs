using System;

namespace bs.component.integrations.Basket
{
    public interface ICreateBasketEvent
    {
        public Guid BasketRef { get; }
        public int ProductId { get; }
        public int Quantity { get; }
        public double ProductPrice { get; }
    }
}
