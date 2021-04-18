using bs.component.integrations.Common;
using System;

namespace bs.component.integrations.Customers
{
    public interface ICustomerCreateEvent
    {
        public Guid CorrelationId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime Dob { get; }
        public int PhoneNumber { get; }
        public string EmailAddress { get; }
        public IAddressEvent BillingAddress { get; }
        public IConsentEvent Consents { get; }
        public ICardDetailEvent? CardDetails { get; }
    }
}
