using bs.component.integrations.Customers;
using System;

namespace bs.order.domain.Models
{
    public class CustomerCreated : ICustomerCreated
    {
        public Guid CorrelationId { get; set; }
        public int CustomerId { get; set; }
    }
}
