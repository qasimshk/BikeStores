using bs.order.domain.Entities;
using MediatR;

namespace bs.order.domain.Events
{
    public class AddCardDetailsDomainEvent : INotification
    {
        public AddCardDetailsDomainEvent(CardDetail cardDetail)
        {
            CardDetails = cardDetail;
        }

        public CardDetail CardDetails { get; init; }
    }
}
