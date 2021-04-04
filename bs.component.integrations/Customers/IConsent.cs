namespace bs.component.integrations.Customers
{
    public interface IConsent
    {
        public bool ContactByEmail { get; }
        public bool ContactByText { get; }
        public bool ContactByCall { get; }
        public bool ContactByPost { get; }
    }
}
