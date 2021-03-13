using System;

namespace bs.order.domain.Exceptions
{
    public class OrderingDomainException : Exception
    {
        public OrderingDomainException(string message) : base(message) { }
    }
}
