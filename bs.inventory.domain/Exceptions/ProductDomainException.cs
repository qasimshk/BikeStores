using System;

namespace bs.inventory.domain.Exceptions
{
    public class ProductDomainException : Exception
    {
        public ProductDomainException(string message) : base(message) { }
    }
}
