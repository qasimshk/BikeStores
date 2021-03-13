using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;
using System;

namespace bs.order.domain.Entities
{
    public class Payment : Entity
    {
        protected Payment() { }

        public Payment(int customerId, Guid paymentRef, double amount, DateTime transactionDate, PaymentType paymentType, TransactionStatus status, int? cardDetailId = default)
        {
            _customerId = customerId;
            _cardDetailId = cardDetailId;

            PaymentRef = paymentRef;
            Amount = amount;
            TransactionDate = transactionDate;
            PaymentType = paymentType;
            Status = status;
        }

        private int _customerId { get; }
        private int? _cardDetailId { get; }

        public Guid PaymentRef { get; private set; }
        public double Amount { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public PaymentType PaymentType { get; private set; }
        public TransactionStatus Status { get; private set; }
    }
}
