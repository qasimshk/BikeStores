using bs.order.domain.Entities;
using MediatR;

namespace bs.order.domain.Events
{
    public class AddOrUpdateCustomerConsentDomainEvent : INotification
    {
        public AddOrUpdateCustomerConsentDomainEvent(bool contactByEmail, bool contactByText, bool contactByCall, bool contactByPost, int customerId)
        {
            Consent = new Consent(contactByEmail, contactByText, contactByCall, contactByPost, customerId);
        }

        public Consent Consent { get; private set; }
    }
}
