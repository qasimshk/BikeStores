using bs.component.integrations.Payments;

namespace bs.order.application.Events
{
    public class PaymentEvent : IPaymentEvent
    {
        public int PaymentType { get; init; }
    }
}
