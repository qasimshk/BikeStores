using System;

namespace bs.component.integrations.Customers
{
    public interface ICustomerCreatedEvent
    {
        public Guid CorrelationId { get; }
        public int CustomerId { get; }
        public int? CardDetailId { get; set; }
    }
}
