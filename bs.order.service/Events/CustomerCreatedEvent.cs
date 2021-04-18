using System;
using bs.component.integrations.Customers;

namespace bs.order.service.Events
{
    public class CustomerCreatedEvent : ICustomerCreatedEvent
    {
        public Guid CorrelationId { get; set; }
        public int CustomerId { get; set; }
        public int? CardDetailId { get; set; }
    }
}
