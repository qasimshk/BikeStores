using bs.component.sharedkernal.Common;

namespace bs.order.domain.Entities
{
    public class Consent : Entity
    {
        protected Consent() { }

        public Consent(bool contactByEmail, bool contactByText, bool contactByCall, bool contactByPost, int customerId)
        {
            ContactByEmail = contactByEmail;
            ContactByText = contactByText;
            ContactByCall = contactByCall;
            ContactByPost = contactByPost;
            _customerId = customerId;
        }

        private int _customerId { get; set; }
        public virtual Customer Customer { get; }
        public bool ContactByEmail { get; private set; }
        public bool ContactByText { get; private set; }
        public bool ContactByCall { get; private set; }
        public bool ContactByPost { get; private set; }
    }
}

