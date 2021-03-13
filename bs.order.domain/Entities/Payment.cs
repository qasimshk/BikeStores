using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;
using System;

namespace bs.order.domain.Entities
{
    public class Payment : Entity
    {
        public Payment(Customer customer, Guid paymentRef, double amount, DateTime transactionDate, PaymentType paymentType, TransactionStatus status, CardDetail cardDetail = null)
        {
            PaymentRef = paymentRef;
            Amount = amount;
            TransactionDate = transactionDate;
            Customer = customer;
            PaymentType = paymentType;
            Status = status;
            CardDetails = cardDetail;
        }

        public Guid PaymentRef { get; private set; }
        public double Amount { get; private set; }
        public DateTime TransactionDate { get; private set; }
        public Customer Customer { get; private set; }
        public PaymentType PaymentType { get; private set; }
        public TransactionStatus Status { get; private set; }
        public CardDetail CardDetails { get; private set; }
    }
}
