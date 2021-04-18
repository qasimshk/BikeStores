using bs.component.integrations.Customers;
using bs.component.integrations.Payments;
using System;

namespace bs.component.integrations.Requests
{
    public interface IOrderSubmitEvent
    {
        public Guid OrderRef { get; }
        public ICustomerCreateEvent Customer { get; }
        public IPaymentEvent Payment { get; }
        public Guid BasketRef { get; }
    }
}
