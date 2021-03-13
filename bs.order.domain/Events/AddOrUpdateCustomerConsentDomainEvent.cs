using bs.component.sharedkernal.Common;
using bs.order.domain.Entities;

namespace bs.order.domain.Events
{
    public class AddOrUpdateCustomerConsentDomainEvent : DomainEventBase
    {
        public AddOrUpdateCustomerConsentDomainEvent(bool contactByEmail, bool contactByText, bool contactByCall, bool contactByPost, int customerId)
        {
            Consent = new Consent(contactByEmail, contactByText, contactByCall, contactByPost, customerId);
        }

        public Consent Consent { get; private set; }
    }
}
