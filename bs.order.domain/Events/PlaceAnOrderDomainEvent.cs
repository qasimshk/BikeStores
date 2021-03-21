using bs.order.domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;

namespace bs.order.domain.Events
{
    public class PlaceAnOrderDomainEvent : INotification
    {
        public PlaceAnOrderDomainEvent(Guid orderRef, int paymentId, int customerId, Address deliveryAddress, List<OrderItem> orderItems)
        {
            Order = new Order(orderRef, paymentId, customerId, deliveryAddress, orderItems);
        }

        public Order Order { get; init; }
    }
}
