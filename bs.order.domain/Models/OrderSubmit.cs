using bs.component.integrations.Customers;
using bs.component.integrations.Orders;
using bs.component.integrations.Payments;
using bs.component.integrations.Requests;
using System;

namespace bs.order.domain.Models
{
    public class OrderSubmit : IOrderSubmitEvent
    {
        public ICustomerEvent Customer { get; init; }

        public IPaymentEvent Payment { get; init; }

        public IOrderEvent Order { get; init; }

        public Guid CorrelationId { get; init; }
    }
}
