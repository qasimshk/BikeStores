using bs.component.integrations.Payments;
using System;

namespace bs.order.service.Events
{
    public class PaymentCreatedEvent : IPaymentCreatedEvent
    {
        public Guid CorrelationId { get; set; }

        public int PaymentId { get; set; }
    }
}
