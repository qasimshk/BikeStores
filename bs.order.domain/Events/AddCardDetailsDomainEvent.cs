using bs.component.sharedkernal.Common;
using bs.order.domain.Entities;
using bs.order.domain.Enums;
using System;

namespace bs.order.domain.Events
{
    public class AddCardDetailsDomainEvent : DomainEventBase
    {
        public AddCardDetailsDomainEvent(string cardHolderName, long cardNumber, DateTime expiration, int securityNumber, CardType cardType, int customerId)
        {
            CardDetails = new CardDetail(cardHolderName, cardNumber, expiration, securityNumber, cardType, customerId);
        }

        public CardDetail CardDetails { get; private set; }
    }
}
