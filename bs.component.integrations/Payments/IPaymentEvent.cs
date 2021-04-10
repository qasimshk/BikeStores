using System;

namespace bs.component.integrations.Payments
{
    public interface IPaymentEvent
    {
        public Guid CorrelationId { get; }
        public Guid PaymentRef { get; }
        public double Amount { get; }
        public int PaymentType { get; }
    }
}
