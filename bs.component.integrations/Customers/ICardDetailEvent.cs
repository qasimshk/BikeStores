using System;

namespace bs.component.integrations.Customers
{
    public interface ICardDetailEvent
    {
        public long CardNumber { get; }
        public int CardType { get; }
        public DateTime Expiry { get; }
        public string CardHolderName { get; }
        public int SecurityNumber { get; }
    }
}
