using System;

namespace bs.component.integrations.Payments
{
    public interface IPaymentCreated
    {
        public Guid CorrelationId { get; }
        public int PaymentId { get; }
        public int? CardDetailId { get; }
    }
}
