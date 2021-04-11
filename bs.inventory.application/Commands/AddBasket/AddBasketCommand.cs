using bs.inventory.domain.Models;
using MediatR;
using System;

namespace bs.inventory.application.Commands.AddBasket
{
    public class AddBasketCommand : IRequest<Unit>
    {
        public AddBasketCommand(AddBasketDto addBasket)
        {
            BasketRef = addBasket.BasketRef;
            BasketItem = addBasket.BasketItems;
        }

        public Guid BasketRef { get; }

        public AddBasketItemDto BasketItem { get; }
    }
}
