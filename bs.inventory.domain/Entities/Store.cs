using bs.component.sharedkernal.Abstractions;
using bs.component.sharedkernal.Common;

namespace bs.inventory.domain.Entities
{
    public class Store : Entity, IAggregateRoot
    {
        protected Store() { }

        public Store(string name, int phone, Address storeAddress)
        {
            Name = name.Trim();
            Phone = phone;
            StoreAddress = storeAddress;
        }

        public string Name { get; private set; }
        public int Phone { get; private set; }
        public Address StoreAddress { get; private set; }
    }
}
