using bs.component.integrations.Common;

namespace bs.order.service.Events
{
    public class AddressEvent : IAddressEvent
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
    }
}