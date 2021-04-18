using bs.component.integrations.Basket;
using System;

namespace bs.inventory.service.Events
{
    public class BasketStatusEvent : IBasketStatusEvent
    {
        public Guid BasketRef { get; set; }
        public Guid OrderRef { get; set; }
        public string CurrentState { get; set; }
        public double BasketPrice { get; set; }
        public string JsonBasketItems { get; set; }
    }
}
