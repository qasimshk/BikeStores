using bs.component.integrations.Customers;
using System;

namespace bs.order.service.Events
{
    public class CardDetailEvent : ICardDetailEvent
    {
        public long CardNumber { get; set; }

        public int CardType { get; set; }

        public DateTime Expiry { get; set; }

        public string CardHolderName { get; set; }

        public int SecurityNumber { get; set; }
    }
}
