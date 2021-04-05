using bs.component.integrations.Customers;
using bs.component.integrations.Orders;
using bs.component.integrations.Payments;
using bs.component.integrations.Requests;
using System;

namespace bs.order.domain.Models
{
    public class OrderSubmit : IOrderSubmit
    {
        public ICustomer Customer { get; init; }

        public IPayment Payment { get; init; }

        public IOrder Order { get; init; }

        public Guid CorrelationId { get; init; }
    }
}
