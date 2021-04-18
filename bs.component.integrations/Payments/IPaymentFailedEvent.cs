using System;

namespace bs.component.integrations.Payments
{
    public interface IPaymentFailedEvent
    {
        public Guid CorrelationId { get; }
        public Guid TransactionRef { get; }
        public int PaymentId { get; }
        public string ErrorMessage { get; }
    }
}
