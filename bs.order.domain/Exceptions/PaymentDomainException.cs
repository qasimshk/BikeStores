using System;

namespace bs.order.domain.Exceptions
{
    public class PaymentDomainException : Exception
    {
        public PaymentDomainException(string message) : base(message) { }
    }
}
