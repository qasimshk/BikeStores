using bs.component.integrations.Basket;
using bs.inventory.domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace bs.inventory.service.Events
{
    public class BasketPlacedSuccessfullyEvent : IBasketPlacedSuccessfullyEvent
    {
        public BasketPlacedSuccessfullyEvent(Guid basketRef, double price, List<JsonBasketItem> basketItems)
        {
            BasketRef = basketRef;
            BasketPrice = price;
            JsonBasketItems = JsonConvert.SerializeObject(basketItems);
        }

        public Guid BasketRef { get; }

        public double BasketPrice { get; }

        public string JsonBasketItems { get; }
    }
}
