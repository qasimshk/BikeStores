using bs.component.integrations.Payments;
using System;

namespace bs.order.service.Events
{
    public class PaymentFailedEvent : IPaymentFailedEvent
    {
        public Guid CorrelationId { get; set; }
        public Guid TransactionRef { get; set; }
        public int PaymentId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
