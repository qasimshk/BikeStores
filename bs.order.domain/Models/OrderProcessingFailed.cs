using bs.component.integrations.Common;
using System;

namespace bs.order.domain.Models
{
    public class OrderProcessingFailed : IOrderProcessingFailed
    {
        public string ErrorMessage { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
