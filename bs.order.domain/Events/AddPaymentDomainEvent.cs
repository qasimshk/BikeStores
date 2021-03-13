using bs.component.sharedkernal.Common;
using bs.order.domain.Entities;
using bs.order.domain.Enums;
using System;

namespace bs.order.domain.Events
{
    public class AddPaymentDomainEvent : DomainEventBase
    {
        public AddPaymentDomainEvent(Customer customer, Guid paymentRef, double amount, DateTime transactionDate, PaymentType paymentType, TransactionStatus status, CardDetail cardDetail = null)
        {
            Payment = new Payment(customer, paymentRef, amount, transactionDate, paymentType, status, cardDetail);
        }

        public Payment Payment { get; private set; }
    }
}
