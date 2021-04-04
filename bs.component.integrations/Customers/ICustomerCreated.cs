using System;

namespace bs.component.integrations.Customers
{
    public interface ICustomerCreated
    {
        public Guid CorrelationId { get; }
        public int CustomerId { get; }
    }
}
