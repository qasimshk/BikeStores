using bs.component.sharedkernal.Abstractions;
using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;
using bs.order.domain.Events;
using bs.order.domain.Exceptions;
using System;
using System.Collections.Generic;

namespace bs.order.domain.Entities
{
    public class Payment : Entity, IAggregateRoot
    {
        protected Payment() { }

        public Payment(int customerId, double amount, PaymentType paymentType, Guid paymentRef, int? cardDetailId = null) : this()
        {
            if (amount is 0)
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
        private int? _cardDetailId;

        public Customer Customer { get; private set; }
        public Order Order { get; private set; }
        public CardDetail CardDetail { get; private set; }
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
            AddDomainEvent(new PlaceAnOrderDomainEvent(new Order(orderRef, Id, _customerId, deliveryAddress, orderItems)));
        }

        public void MarkTransactionAsDeclined()
        {
            if (Status != TransactionStatus.Processing) throw new PaymentDomainException("Payment is already processed");
            
            Status = TransactionStatus.Declined;
            TransactionDate = DateTime.Now.Date;
        }

        public void MarkTransactionAsRefunded()
        {
            if (Status != TransactionStatus.Successful) throw new PaymentDomainException("This payment was not successfully completed");

            Status = TransactionStatus.Refund;
            RefundedOn = DateTime.Now.Date;
        }
    }
}
