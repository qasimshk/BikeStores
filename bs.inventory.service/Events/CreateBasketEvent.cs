using bs.component.integrations.Basket;
using System;

namespace bs.inventory.service.Events
{
    public class CreateBasketEvent : ICreateBasketEvent
    {
        private readonly ISubmitBasketEvent _submitBasket;

        public CreateBasketEvent(ISubmitBasketEvent submitBasket)
        {
            _submitBasket = submitBasket;
        }

        public Guid BasketRef => _submitBasket.BasketRef;

        public int ProductId => _submitBasket.ProductId;

        public int Quantity => _submitBasket.Quantity;

        public double ProductPrice => _submitBasket.ProductPrice;
    }
}
