using System;

namespace bs.component.integrations.Payments
{
    public interface IPayment
    {
        public Guid CorrelationId { get; }
        public Guid PaymentRef { get; }
        public double Amount { get; }
        public int PaymentType { get; }
    }
}
