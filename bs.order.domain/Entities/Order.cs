using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;
using System;
using System.Collections.Generic;
using bs.order.domain.Events;
using bs.order.domain.Exceptions;

namespace bs.order.domain.Entities
{
    public class Order : Entity
    {
        protected Order() { }

        public Order(Guid orderRef, int paymentId, int customerId, Address deliveryAddress, List<OrderItem> orderItems)
        {
            if (paymentId is 0)
            {
                throw new OrderingDomainException("Invalid payment Id");
            }

            if (customerId is 0)
            {
                throw new OrderingDomainException("Invalid customer Id");
            }

            _paymentId = paymentId;
            _customerId = customerId;

            OrderRef = orderRef;
            Status = OrderStatus.Paid;
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
