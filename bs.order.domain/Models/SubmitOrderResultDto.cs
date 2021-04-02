using System;
using bs.order.domain.Enums;

namespace bs.order.domain.Models
{
    public class SubmitOrderResultDto
    {
        public Guid OrderRef { get; set; }
        public DateTime OrderSubmittedOn { get; set; }
        public OrderStatus OrderStatus { get; set; }
    }
}
