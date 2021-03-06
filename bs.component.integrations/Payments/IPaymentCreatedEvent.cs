using System;

namespace bs.component.integrations.Payments
{
    public interface IPaymentCreatedEvent
    {
        public Guid CorrelationId { get; }
        public Guid TransactionRef { get; }
        public int PaymentId { get; }
    }
}
