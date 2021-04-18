using System;

namespace bs.order.domain.Models
{
    public class SubmitOrderResultDto
    {   
        public Guid OrderRef { get; init; }
        public string OrderStatus { get; init; }
        public DateTime OrderSubmittedOn => DateTime.Now.Date;
        public string Message => "Order request submitted successfully";
    }
}
