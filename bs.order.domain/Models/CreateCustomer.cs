using bs.component.integrations.Common;
using bs.component.integrations.Customers;
using System;
using System.Collections.Generic;

namespace bs.order.domain.Models
{
    public class CreateCustomer : ICustomer
    {
        public Guid CorrelationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public int PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public IAddress BillingAddress { get; set; }
        public IConsent Consents { get; set; }
        public IList<ICardDetail> CardDetails { get; set; }
    }
}
