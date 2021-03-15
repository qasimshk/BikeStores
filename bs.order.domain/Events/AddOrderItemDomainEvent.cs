using bs.order.domain.Entities;
using System;
using MediatR;

namespace bs.order.domain.Events
{
    public class AddOrderItemDomainEvent : INotification
    {
        public AddOrderItemDomainEvent(Guid productRef, string productName, int quantity, double individualPrice, int orderId)
        {
            OrderItem = new OrderItem(productRef, productName, quantity, individualPrice, orderId);
        }

        public OrderItem OrderItem { get; init; }
    }
}
