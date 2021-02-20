using System;

namespace bs.component.sharedkernal.Exceptions
{
    public class EntityDomainException : Exception
    {
        public EntityDomainException() { }

        public EntityDomainException(string message) : base(message) { }

        public EntityDomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
