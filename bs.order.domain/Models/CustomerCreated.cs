using bs.component.integrations.Customers;
using System;

namespace bs.order.domain.Models
{
    public class CustomerCreated : ICustomerCreatedEvent
    {
        public Guid CorrelationId { get; set; }
        public int CustomerId { get; set; }
    }
}
