using bs.component.sharedkernal.Common;

namespace bs.order.domain.Entities
{
    public class Consent : Entity
    {
        public Consent(Customer customer, bool contactByEmail, bool contactByText, bool contactByCall, bool contactByPost)
        {
            Customer = customer;
            ContactByEmail = contactByEmail;
            ContactByText = contactByText;
            ContactByCall = contactByCall;
            ContactByPost = contactByPost;
        }

        public Customer Customer { get; private set; }
        public bool ContactByEmail { get; private set; }
        public bool ContactByText { get; private set; }
        public bool ContactByCall { get; private set; }
        public bool ContactByPost { get; private set; }
    }
}

