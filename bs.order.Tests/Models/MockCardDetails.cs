using bs.order.domain.Enums;
using System;

namespace bs.order.Tests.Models
{
    public class MockCardDetails
    {
        public long CardNumber { get; set; }
        public DateTime Expiration { get; set; }
        public int SecurityNumber { get; set; }
        public CardType CardType { get; set; }
        public string CardHolderName { get; set; }
    }
}
