namespace bs.component.integrations.Common
{
    public interface IAddressEvent
    {
        public string Street { get; }
        public string City { get; }
        public string Country { get; }
        public string PostCode { get; }
    }
}
