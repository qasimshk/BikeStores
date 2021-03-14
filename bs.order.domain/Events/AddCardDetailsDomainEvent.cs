using bs.order.domain.Entities;
using bs.order.domain.Enums;
using MediatR;
using System;

namespace bs.order.domain.Events
{
    public class AddCardDetailsDomainEvent : INotification
    {
        public AddCardDetailsDomainEvent(string cardHolderName, long cardNumber, DateTime expiration, int securityNumber, CardType cardType, int customerId)
        {
            CardDetails = new CardDetail(cardHolderName, cardNumber, expiration, securityNumber, cardType, customerId);
        }

        public CardDetail CardDetails { get; private set; }
    }
}
