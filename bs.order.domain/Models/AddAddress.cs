using bs.component.integrations.Common;

namespace bs.order.domain.Models
{
    public class AddAddress : IAddress
    {
        private readonly IAddress _address;

        public AddAddress(IAddress address)
        {
            _address = address;
        }

        public string Street => _address.Street;
        public string City => _address.City;
        public string Country => _address.Country;
        public string PostCode => _address.PostCode;
    }
}
