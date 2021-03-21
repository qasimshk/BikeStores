using bs.order.domain.Entities;
using MediatR;

namespace bs.order.domain.Events
{
    public class AddOrUpdateCustomerConsentDomainEvent : INotification
    {
        public AddOrUpdateCustomerConsentDomainEvent(Consent consent)
        {
            Consent = consent;
        }

        public Consent Consent { get; init; }
    }
}
