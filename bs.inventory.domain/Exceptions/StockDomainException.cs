using System;

namespace bs.inventory.domain.Exceptions
{
    public class StockDomainException : Exception
    {
        public StockDomainException(string message) : base(message) { }
    }
}
