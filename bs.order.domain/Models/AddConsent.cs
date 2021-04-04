using bs.component.integrations.Customers;

namespace bs.order.domain.Models
{
    public class AddConsent : IConsent
    {
        private readonly IConsent _consent;

        public AddConsent(IConsent consent)
        {
            _consent = consent;
        }

        public bool ContactByEmail => _consent.ContactByEmail;
        public bool ContactByText => _consent.ContactByText;
        public bool ContactByCall => _consent.ContactByCall;
        public bool ContactByPost => _consent.ContactByPost;
    }
}
