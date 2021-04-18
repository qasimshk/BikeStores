using System;
using bs.component.integrations.Orders;

namespace bs.order.service.Events
{
    public class OrderItemsEvent : IOrderItemsEvent
    {
        public Guid ProductRef { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}