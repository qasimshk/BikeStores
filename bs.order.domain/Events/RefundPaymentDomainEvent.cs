using MediatR;

namespace bs.order.domain.Events
{
    public class RefundPaymentDomainEvent : INotification
    {
        public RefundPaymentDomainEvent(int paymentId)
        {
            PaymentId = paymentId;
        }

        public int PaymentId { get; init; }
    }
}
