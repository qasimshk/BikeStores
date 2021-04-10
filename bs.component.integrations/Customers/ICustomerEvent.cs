using bs.component.integrations.Common;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace bs.component.integrations.Customers
{
    public interface ICustomerEvent
    {
        public Guid CorrelationId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime Dob { get; }
        public int PhoneNumber { get; }
        public string EmailAddress { get; }
        public IAddressEvent BillingAddress { get; }
        public IConsentEvent Consents { get; }
        public IList<ICardDetailEvent>? CardDetails { get; }
    }
}
