using bs.component.integrations.Common;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace bs.component.integrations.Customers
{
    public interface ICustomer
    {
        public Guid CorrelationId { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime Dob { get; }
        public string PhoneNumber { get; }
        public string EmailAddress { get; }
        public IAddress BillingAddress { get; }
        public IConsent Consents { get; }
        public IList<ICardDetail>? CardDetails { get; }
    }
}
