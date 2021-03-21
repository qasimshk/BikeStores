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
        Declined,
        Processing,
        Refund
    }

    public enum OrderStatus
    {
        Paid,
        Delivered,
        Cancelled, //If order is Delivered then it cannot be cancelled
        Refund
    }

    public enum CardType
    {
        Master,
        Visa,
        AmericanExpress,
        None
    }
}
