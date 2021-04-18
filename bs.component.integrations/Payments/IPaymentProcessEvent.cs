using System;

namespace bs.component.integrations.Payments
{
    public interface IPaymentProcessEvent
    {
        public int CustomerId { get; set; }
        public Guid CorrelationId { get; }
        public double Amount { get; }
        public int PaymentType { get; }
        public int? CardDetailsId { get; }
        public string JsonCardDetails { get; }
    }
}
