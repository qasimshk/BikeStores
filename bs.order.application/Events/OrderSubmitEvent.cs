using bs.component.integrations.Customers;
using bs.component.integrations.Payments;
using bs.component.integrations.Requests;
using System;

namespace bs.order.application.Events
{
    public class OrderSubmitEvent : IOrderSubmitEvent
    {
        public Guid OrderRef { get; init; }

        public ICustomerCreateEvent Customer { get; init; }

        public IPaymentEvent Payment { get; init; }

        public Guid BasketRef { get; init; }
    }
}
