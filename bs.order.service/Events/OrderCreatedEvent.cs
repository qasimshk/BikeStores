using bs.component.integrations.Orders;
using System;

namespace bs.order.service.Events
{
    public class OrderCreatedEvent : IOrderCreatedEvent
    {
        public Guid OrderRef { get; set; }
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
    }
}
