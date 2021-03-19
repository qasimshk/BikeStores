using bs.component.sharedkernal.Abstractions;
using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;
using bs.order.domain.Exceptions;
using System;
using bs.order.domain.Events;
using System.Collections.Generic;

namespace bs.order.domain.Entities
{
    public class Payment : Entity, IAggregateRoot
    {
        protected Payment() { }

        public Payment(int customerId, double amount, PaymentType paymentType, Guid paymentRef, int? cardDetailId = default)
        {
            if (customerId <= 0)
            {
                throw new PaymentDomainException("Invalid Customer");
            }

            if (amount <= 0)
            {
                throw new PaymentDomainException("Insufficient Amount");
            }
            
            _customerId = customerId;
            _cardDetailId = cardDetailId;
            
            Amount = amount;
            PaymentType = paymentType;
            PaymentRef = paymentRef;
            Status = TransactionStatus.Processing;
        }

        private readonly int _customerId;
        private readonly int? _cardDetailId;

        public Customer Customer { get; }
        public Order Order { get; }
        public CardDetail CardDetail { get; }
        public Guid PaymentRef { get; private set; }
        public double Amount { get; private set; }
        public DateTime? TransactionDate { get; private set; }
        public PaymentType PaymentType { get; private set; }
        public TransactionStatus Status { get; private set; }
        public DateTime? RefundedOn { get; private set; }
        
        public void MarkTransactionSuccessfulAndPlaceAnOrder(Guid orderRef, Address deliveryAddress, List<OrderItem> orderItems)
        {
            Status = TransactionStatus.Successful;
            TransactionDate = DateTime.Now.Date;
            AddDomainEvent(new PlaceAnOrderDomainEvent(orderRef,OrderStatus.Paid,Id, _customerId, deliveryAddress, orderItems));
        }

        public void MarkTransactionAsDeclined()
        {
            Status = TransactionStatus.Declined;
            TransactionDate = DateTime.Now.Date;
        }

        public void MarkTransactionAsRefunded()
        {
            Status = TransactionStatus.Refunded;
            RefundedOn = DateTime.Now.Date;
        }
    }
}
