using System;
using bs.order.domain.Enums;

namespace bs.order.domain.Models
{
    public class SubmitOrderResultDto
    {
        public Guid CorrelationId { get; init; }
        public Guid PaymentRef { get; init; }
        public Guid OrderRef { get; init; }
        public string OrderStatus { get; init; }
        public DateTime OrderSubmittedOn => DateTime.Now.Date;
        public string Message => "Order request submitted successfully";
    }
}
