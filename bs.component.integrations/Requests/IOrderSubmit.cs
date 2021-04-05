using System;
using bs.component.integrations.Customers;
using bs.component.integrations.Orders;
using bs.component.integrations.Payments;

namespace bs.component.integrations.Requests
{
    public interface IOrderSubmit
    {
        public Guid CorrelationId { get; }
        public ICustomer Customer { get; }
        public IPayment Payment { get; }
        public IOrder Order { get; }
    }
}
