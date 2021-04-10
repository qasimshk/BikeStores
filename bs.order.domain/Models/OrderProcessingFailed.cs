using bs.component.integrations.Common;
using System;

namespace bs.order.domain.Models
{
    public class OrderProcessingFailed : IOrderProcessingFailedEvent
    {
        public string ErrorMessage { get; set; }
        public Guid CorrelationId { get; set; }
    }
}
