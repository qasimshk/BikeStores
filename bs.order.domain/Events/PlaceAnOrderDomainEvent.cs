using bs.order.domain.Entities;
using bs.order.domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;

namespace bs.order.domain.Events
{
    public class PlaceAnOrderDomainEvent : INotification
    {
        public PlaceAnOrderDomainEvent(Guid orderRef, OrderStatus status, int paymentId, int customerId, Address deliveryAddress, List<OrderItem> orderItems)
        {
            Order = new Order(orderRef, status, paymentId, customerId, deliveryAddress, orderItems);
        }

        public Order Order { get; init; }
    }
}
