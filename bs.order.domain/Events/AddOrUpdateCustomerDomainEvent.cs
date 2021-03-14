using bs.order.domain.Entities;
using MediatR;

namespace bs.order.domain.Events
{
    public class AddOrUpdateCustomerDomainEvent : INotification
    {
        public AddOrUpdateCustomerDomainEvent(Customer customer)
        {
            Customer = customer;
        }

        public Customer Customer { get; private set; }
    }
}
