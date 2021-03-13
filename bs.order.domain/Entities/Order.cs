using System;
using System.Collections.Generic;
using bs.component.sharedkernal.Abstractions;
using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;

namespace bs.order.domain.Entities
{
    public class Order : Entity, IAggregateRoot
    {
        protected Order() { }

        public Order(Guid orderRef, OrderStatus status, Payment payment, Customer customer, Address deliveryAddress, List<OrderItem> orderItems)
        {
            OrderRef = orderRef;
            Status = status;
            Payment = payment;
            Customer = customer;
            DeliveryAddress = deliveryAddress;
            OrderItems = orderItems;
        }

        public Guid OrderRef { get; private set; }
        public OrderStatus Status { get; private set; }
        public Payment Payment { get; private set; }
        public Customer Customer { get; private set; }
        public Address DeliveryAddress { get; private set; }
        public IReadOnlyCollection<OrderItem> OrderItems { get; private set; }

    }
}
