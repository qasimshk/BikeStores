using System;
using bs.component.integrations.Customers;

namespace bs.order.application.Events
{
    public class AddCardDetailEvent : ICardDetailEvent
    {
        public long CardNumber { get; init; }
        public int CardType { get; init; }
        public DateTime Expiry { get; init; }
        public string CardHolderName { get; init; }
        public int SecurityNumber { get; init; }
    }
}