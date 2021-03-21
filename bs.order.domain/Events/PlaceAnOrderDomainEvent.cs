using bs.order.domain.Entities;
using MediatR;

namespace bs.order.domain.Events
{
    public class PlaceAnOrderDomainEvent : INotification
    {
        public PlaceAnOrderDomainEvent(Order order)
        {
            Order = order;
        }

        public Order Order { get; init; }
    }
}
