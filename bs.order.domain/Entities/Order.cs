using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;
using System;
using System.Collections.Generic;

namespace bs.order.domain.Entities
{
    public class Order : Entity
    {
        protected Order() { }

        public Order(Guid orderRef, OrderStatus status, int paymentId, int customerId, Address deliveryAddress, List<OrderItem> orderItems)
        {
            _paymentId = paymentId;
            _customerId = customerId;

            OrderRef = orderRef;
            Status = status;
            DeliveryAddress = deliveryAddress;
            OrderItems = orderItems;

            // TODO: Add a foreach loop to get the list of order items and pass it through a domain event to order items
        }

        private readonly int _paymentId;
        private readonly int _customerId;

        public Guid OrderRef { get; private set; }
        public OrderStatus Status { get; private set; }
        public Payment Payment { get; }
        public Customer Customer { get; }
        public Address DeliveryAddress { get; private set; }
        public IReadOnlyCollection<OrderItem> OrderItems { get; private set; }
    }
}
