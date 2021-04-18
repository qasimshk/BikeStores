using System;
using bs.order.domain.Models;
using MediatR;

namespace bs.order.application.Commands.SubmitOrder
{
    public class SubmitOrderCommand : IRequest<SubmitOrderResultDto>
    {
        public Guid OrderRef;
        public Guid BasketRef { get; }
        public CustomerDto Customer { get; }
        public PaymentDto Payment { get; }
        
        public SubmitOrderCommand(SubmitOrderDto submitOrder)
        {
            OrderRef = Guid.NewGuid();
            Customer = submitOrder.Customer;
            Payment = submitOrder.PaymentRequest;
            BasketRef = submitOrder.BasketRef;
        }
    }
}
