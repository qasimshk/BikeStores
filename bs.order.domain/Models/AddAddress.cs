using bs.component.integrations.Common;

namespace bs.order.domain.Models
{
    public class AddAddress : IAddress
    {
        public string Street { get; init; }
        public string City { get; init; }
        public string Country { get; init; }
        public string PostCode { get; init; }
    }
}
