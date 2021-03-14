﻿using bs.component.sharedkernal.Common;
using bs.order.domain.Enums;
using bs.order.domain.Exceptions;
using System;

namespace bs.order.domain.Entities
{
    public class CardDetail : Entity
    {
        protected CardDetail()
        {
            CardType = CardType.None;
        }

        public CardDetail(string cardHolderName, long cardNumber, DateTime expiration, int securityNumber, CardType cardType, int customerId)
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
            _customerId = customerId;
        }

        private readonly long _cardNumber;
        private readonly DateTime _expiration;
        private readonly int _securityNumber;
        private readonly int _customerId;

        public virtual Customer Customer { get; }
        public CardType CardType { get; private set; }
        public string CardHolderName { get; private set; }
        public string CardNumber => $"xxxx xxxx xxxx {_cardNumber.ToString().Substring(12, 4)}";
        public string Expiration => _expiration.ToString("MM/yy");
    }
}
