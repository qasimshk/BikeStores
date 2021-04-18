using System;
using bs.component.integrations.Payments;

namespace bs.order.service.Events
{
    public class PaymentProcessEvent : IPaymentProcessEvent
    {
        public Guid CorrelationId { get; set; }
        public int PaymentType { get; set; }
        public double Amount { get; set; }
        public int CustomerId { get; set; }
        public int? CardDetailsId { get; set; }
        public string JsonCardDetails { get; set; }
    }
}
