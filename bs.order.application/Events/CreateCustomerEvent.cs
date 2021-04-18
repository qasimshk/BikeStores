using bs.component.integrations.Common;
using bs.component.integrations.Customers;
using System;

namespace bs.order.application.Events
{
    public class CreateCustomerEvent : ICustomerCreateEvent
    {
        public Guid CorrelationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Dob { get; set; }
        public int PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public IAddressEvent BillingAddress { get; set; }
        public IConsentEvent Consents { get; set; }
        public ICardDetailEvent CardDetails { get; set; }
    }
}
