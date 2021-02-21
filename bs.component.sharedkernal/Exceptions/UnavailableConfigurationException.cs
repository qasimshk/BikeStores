using System;

namespace bs.component.sharedkernal.Exceptions
{
    public class UnavailableConfigurationException : Exception
    {
        public UnavailableConfigurationException() : base() { }

        public UnavailableConfigurationException(string message) : base(message) { }
    }
}
