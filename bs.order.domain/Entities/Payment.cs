using bs.component.sharedkernal.Abstractions;
using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;
using bs.order.domain.Exceptions;
using System;

namespace bs.order.domain.Entities
{
    public class Payment : Entity, IAggregateRoot
    {
        protected Payment() { }

        public Payment(int customerId, double amount, PaymentType paymentType, int? cardDetailId = default)
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
            Status = TransactionStatus.Processing;
        }

        private readonly int _customerId;
        private readonly int? _cardDetailId;

        public Customer Customer { get; }
        public CardDetail CardDetail { get; }
        public Guid PaymentRef { get; private set; }
        public double Amount { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public PaymentType PaymentType { get; private set; }
        public TransactionStatus Status { get; private set; }
        

        public void MarkTransactionSuccessfulAndPlaceAnOrder(Guid paymentRef)
        {
            if (paymentRef == Guid.Empty)
            {
                throw new PaymentDomainException("Payment reference cannot be empty");
            }

            PaymentRef = paymentRef;
            Status = TransactionStatus.Successful;
            TransactionDate = DateTime.Now.Date;
            // TODO: Call order domain event here
        }

        public void MarkTransactionAsDeclined(Guid paymentRef)
        {
            if (paymentRef == Guid.Empty)
            {
                throw new PaymentDomainException("Payment reference cannot be empty");
            }

            PaymentRef = paymentRef;
            Status = TransactionStatus.Declined;
            TransactionDate = DateTime.Now.Date;
        }
    }
}
