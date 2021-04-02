namespace bs.order.domain.Enums
{
    public enum PaymentType
    {
        Cash = 1,
        Card = 2
    }

    public enum TransactionStatus
    {
        Successful = 1,
        Declined = 2,
        Processing = 3,
        Refund = 4
    }

    public enum OrderStatus
    {
        Paid = 1,
        Delivered = 2,
        Cancelled = 3, //If order is Delivered then it cannot be cancelled
        Refund = 4
    }

    public enum CardType
    {
        Master = 1,
        Visa = 2,
        AmericanExpress = 3,
        None = 4
    }
}
