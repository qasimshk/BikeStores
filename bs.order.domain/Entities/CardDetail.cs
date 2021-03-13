using System;
using System.Linq;
using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;
using bs.order.domain.Events;
using bs.order.domain.Exceptions;

namespace bs.order.domain.Entities
{
    public class CardDetail : Entity
    {
        public CardDetail(string cardHolderName, long cardNumber, DateTime expiration, int securityNumber, CardType cardType, Customer customer)
        {
            if (expiration.Date < DateTime.Now.Date)
            {
                throw new OrderingDomainException("This card is no longer valid");
            }

            _cardNumber = cardNumber;
            _expiration = expiration;
            _securityNumber = securityNumber;

            CardHolderName = cardHolderName;
            CardType = cardType;
            Customer = customer;
        }

        private long _cardNumber { get; }
        private DateTime _expiration { get; }
        private int _securityNumber { get; }

        public CardType CardType { get; private set; }
        public string CardHolderName { get; private set; }
        public string CardNumber => $"xxxx xxxx xxxx {_cardNumber.ToString().Substring(12, 4)}";
        public string Expiration => _expiration.ToString("MM/yy");
        public Customer Customer { get; private set; }

        public void Pay(Guid paymentRef, double amount, DateTime transactionDate, PaymentType paymentType, TransactionStatus status)
        {
            AddDomainEvent(new AddPaymentDomainEvent(Customer,paymentRef,amount,transactionDate,paymentType,status,this));
        }

        public bool CheckCardExistance() => Customer.CardDetails.Any(c => c._cardNumber == _cardNumber);
    }
}
