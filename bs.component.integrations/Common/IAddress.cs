namespace bs.component.integrations.Common
{
    public interface IAddress
    {
        public string Street { get; }
        public string City { get; }
        public string Country { get; }
        public string PostCode { get; }
    }
}
