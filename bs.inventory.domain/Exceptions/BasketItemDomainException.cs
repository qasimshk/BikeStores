using System;

namespace bs.inventory.domain.Exceptions
{
    public class BasketItemDomainException : Exception
    {
        public BasketItemDomainException(string message) : base(message) { }
    }
}
