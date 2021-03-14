namespace bs.order.domain.Enums
{
    public enum PaymentType
    {
        Cash,
        Card
    }

    public enum TransactionStatus
    {
        Successful,
        Failed
    }

    public enum OrderStatus
    {
        Paid,
        Delivered,
        Cancelled //If order is Delivered then it cannot be cancelled
    }

    public enum CardType
    {
        Master,
        Visa,
        AmericanExpress,
        None
    }
}
