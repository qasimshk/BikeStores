using bs.order.domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;

namespace bs.order.domain.Events
{
    public class PlaceAnOrderDomainEvent : INotification
    {
        public Guid OrderRef { get; }
        public int PaymentId { get; }
        public int CustomerId { get; }
        public Address DeliveryAddress { get; }
        public List<OrderItem> OrderItems { get; }
        public Payment Payment { get; }


        public PlaceAnOrderDomainEvent(Guid orderRef, int paymentId, int customerId, Address deliveryAddress, List<OrderItem> orderItems, Payment payment)
        {
            OrderRef = orderRef;
            PaymentId = paymentId;
            CustomerId = customerId;
            DeliveryAddress = deliveryAddress;
            OrderItems = orderItems;
            Payment = payment;
        }
    }
}
