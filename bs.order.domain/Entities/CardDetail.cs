using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;
using bs.order.domain.Exceptions;
using System;
using System.Collections.Generic;

namespace bs.order.domain.Entities
{
    public class CardDetail : Entity
    {
        private readonly List<Payment> _payments;
        protected CardDetail()
        {
            CardType = CardType.None;
            _payments = new List<Payment>();
        }

        public CardDetail(string cardHolderName, long cardNumber, DateTime expiration, int securityNumber, CardType cardType, int customerId) : this()
        {
            if (expiration.Date < DateTime.Now.Date)
            {
                throw new OrderingDomainException("This card is no longer valid");
            }

            _cardNumber = cardNumber.ToString();
            CardNumberUnFormatted = cardNumber;
            _expiration = expiration;
            _securityNumber = securityNumber;

            CardHolderName = cardHolderName;
            CardType = cardType;
            _customerId = customerId;
        }

        private readonly string _cardNumber;
        private readonly DateTime _expiration;
        private readonly int _securityNumber;
        private readonly int _customerId;

        public virtual Customer Customer { get; }
        public IReadOnlyCollection<Payment> Payments => _payments;
        public CardType CardType { get; private set; }
        public string CardHolderName { get; private set; }

        public string GetCardNumber => MaskCardNumber(_cardNumber);
        public string GetExpiration => _expiration.ToString("MM/yy");
        public long CardNumberUnFormatted { get; private set; }

        string MaskCardNumber(string cardNumber)
        {
            return $"xxxx xxxx xxxx {cardNumber.Substring(12, 4)}";
        }
    }
}
