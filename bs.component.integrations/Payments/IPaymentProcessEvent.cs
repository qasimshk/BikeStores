using bs.component.integrations.Customers;
using System;

namespace bs.component.integrations.Payments
{
    public interface IPaymentProcessEvent
    {
        public int CustomerId { get; }
        public Guid CorrelationId { get; }
        public double Amount { get; }
        public int PaymentType { get; }
        public int? CardDetailsId { get; }
        public ICardDetailEvent CardDetail { get; }
    }
}
