using System;
using bs.order.domain.Models;
using MediatR;

namespace bs.order.application.Commands.SubmitOrder
{
    public class SubmitOrderCommand : IRequest<SubmitOrderResultDto>
    {
        public Guid CorrelationId;
        public Guid OrderRef;
        public Guid PaymentRef { get; set; }
        public CustomerDto Customer { get; }
        public PaymentDto Payment { get; }
        public OrderDto Order { get; }
        
        public SubmitOrderCommand(SubmitOrderDto submitOrder)
        {
            CorrelationId = Guid.NewGuid();
            OrderRef = Guid.NewGuid();

            Customer = submitOrder.Customer;
            Payment = submitOrder.PaymentRequest;
            Order = submitOrder.Order;
        }
    }
}
