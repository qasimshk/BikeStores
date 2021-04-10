using System;

namespace bs.component.integrations.Payments
{
    public interface IPaymentCreatedEvent
    {
        public Guid CorrelationId { get; }
        public int PaymentId { get; }
        public int? CardDetailId { get; }
    }
}
