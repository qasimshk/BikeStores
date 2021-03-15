using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;
using System;
using System.Collections.Generic;
using bs.order.domain.Events;

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

            foreach (var item in orderItems)
            {
                AddDomainEvent(new AddOrderItemDomainEvent(item.ProductRef, item.ProductName, item.Quantity, item.IndividualPrice, Id));
            }
        }

        private readonly int _paymentId;
        private readonly int _customerId;

        public Guid OrderRef { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime? CancelledOn { get; private set; }
        public DateTime? DeliveredOn { get; private set; }
        public string ReasonOfCancellation { get; private set; }
        public Payment Payment { get; }
        public Customer Customer { get; }
        public Address DeliveryAddress { get; private set; }
        public IReadOnlyCollection<OrderItem> OrderItems { get; private set; }

        public void OrderCancelled(string reason)
        {
            Status = OrderStatus.Cancelled;
            CancelledOn = DateTime.Now.Date;
            ReasonOfCancellation = reason.Trim();
            AddDomainEvent(new RefundPaymentDomainEvent(_paymentId));
        }

        public void OrderDelivered()
        {
            Status = OrderStatus.Delivered;
            DeliveredOn = DateTime.Now.Date;
        }
    }
}
