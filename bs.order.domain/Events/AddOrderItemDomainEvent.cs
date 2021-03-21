using bs.order.domain.Entities;
using MediatR;

namespace bs.order.domain.Events
{
    public class AddOrderItemDomainEvent : INotification
    {
        public AddOrderItemDomainEvent(OrderItem orderItem)
        {
            OrderItem = orderItem;
        }

        public OrderItem OrderItem { get; init; }
    }
}
