using bs.component.integrations.Customers;

namespace bs.order.domain.Models
{
    public class AddConsent : IConsent
    {
        public bool ContactByEmail { get; init; }
        public bool ContactByText { get; init; }
        public bool ContactByCall { get; init; }
        public bool ContactByPost { get; init; }
    }
}
