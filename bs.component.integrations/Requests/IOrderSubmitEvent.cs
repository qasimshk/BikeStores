using System;
using bs.component.integrations.Customers;
using bs.component.integrations.Orders;
using bs.component.integrations.Payments;

namespace bs.component.integrations.Requests
{
    public interface IOrderSubmitEvent
    {
        public Guid CorrelationId { get; }
        public ICustomerEvent Customer { get; }
        public IPaymentEvent Payment { get; }
        public IOrderEvent Order { get; }
    }
}
