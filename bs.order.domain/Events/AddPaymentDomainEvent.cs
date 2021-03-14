using bs.order.domain.Entities;
using bs.order.domain.Enums;
using MediatR;
using System;

namespace bs.order.domain.Events
{
    public class AddPaymentDomainEvent : INotification
    {
        public AddPaymentDomainEvent(int customerId, Guid paymentRef, double amount, DateTime transactionDate, PaymentType paymentType, TransactionStatus status, int? cardDetailId = default)
        {
            Payment = new Payment(customerId, paymentRef, amount, transactionDate, paymentType, status, cardDetailId);
        }

        public Payment Payment { get; private set; }
    }
}
