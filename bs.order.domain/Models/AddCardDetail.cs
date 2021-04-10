using System;
using bs.component.integrations.Customers;

namespace bs.order.domain.Models
{
    public class AddCardDetail : ICardDetailEvent
    {
        public long CardNumber { get; init; }
        public int CardType { get; init; }
        public DateTime Expiry { get; init; }
        public string CardHolderName { get; init; }
        public int SecurityNumber { get; init; }
    }
}