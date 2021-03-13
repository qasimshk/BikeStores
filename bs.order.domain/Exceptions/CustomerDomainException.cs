using System;

namespace bs.order.domain.Exceptions
{
    public class CustomerDomainException : Exception
    {
        public CustomerDomainException(string message) : base(message) { }
    }
}
