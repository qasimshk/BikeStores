using bs.component.integrations.Common;
using System;

namespace bs.order.service.Events
{
    public class OrderProcessingFailedEvent : IOrderProcessingFailedEvent
    {
        public Guid OrderRef { get; set; }

        public string ErrorMessage { get; set; }
    }
}
