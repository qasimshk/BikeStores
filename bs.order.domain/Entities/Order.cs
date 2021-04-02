using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;
using bs.order.domain.Events;
using bs.order.domain.Exceptions;
using System;
using System.Collections.Generic;
using bs.component.sharedkernal.Abstractions;

namespace bs.order.domain.Entities
{
    public class Order : Entity, IAggregateRoot
    {
        protected Order() { }

        public Order(Guid orderRef, int paymentId, int customerId, Address deliveryAddress, List<OrderItem> orderItems)
        {
            _paymentId = paymentId;
            _customerId = customerId;

            OrderRef = orderRef;
            Status = OrderStatus.Paid;
            DeliveryAddress = deliveryAddress;

            foreach (var item in orderItems)
            {
                OrderItems = new List<OrderItem>
                {
                    new(item.ProductRef, item.ProductName, item.Quantity, item.IndividualPrice, Id)
                };
            }
        }

        private readonly int _paymentId;
        private readonly int _customerId;

        public Guid OrderRef { get; private set; }
        public OrderStatus Status { get; private set; }
        public DateTime? CancelledOn { get; private set; }
        public DateTime? DeliveredOn { get; private set; }
        public string ReasonOfCancellation { get; private set; }
        public Payment Payment { get; private set; }
        public Customer Customer { get; private set; }
        public Address DeliveryAddress { get; private set; }
        public IList<OrderItem> OrderItems { get; private set; }
        
        public void MarkOrderCancelled(string reason)
        {
            if (Status != OrderStatus.Paid) throw new OrderingDomainException("Order can only be cancelled once paid");

            Status = OrderStatus.Cancelled;
            CancelledOn = DateTime.Now.Date;
            ReasonOfCancellation = reason.Trim();
            AddDomainEvent(new RefundPaymentDomainEvent(_paymentId));
        }

        public void MarkOrderRefund(string reason)
        {
            if (Status != OrderStatus.Delivered) throw new OrderingDomainException("Order can only be refunded once delivered");

            Status = OrderStatus.Refund;
            CancelledOn = DateTime.Now.Date;
            ReasonOfCancellation = reason.Trim();
            AddDomainEvent(new RefundPaymentDomainEvent(_paymentId));
        }

        public void MarkOrderDelivered()
        {
            Status = OrderStatus.Delivered;
            DeliveredOn = DateTime.Now.Date;
        }
    }
}
