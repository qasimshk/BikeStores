using bs.component.sharedkernal.Common;
using bs.order.domain.Entities;

namespace bs.order.domain.Events
{
    public class AddOrUpdateCustomerDomainEvent : DomainEventBase
    {
        public AddOrUpdateCustomerDomainEvent(Customer customer)
        {
            Customer = customer;
        }

        public Customer Customer { get; private set; }
    }
}
