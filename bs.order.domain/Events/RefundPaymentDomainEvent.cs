using MediatR;

namespace bs.order.domain.Events
{
    public class RefundPaymentDomainEvent : INotification
    {
        public int PaymentId { get; }

        public RefundPaymentDomainEvent(int paymentId)
        {
            PaymentId = paymentId;
        }
    }
}
