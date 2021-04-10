using System;

namespace bs.component.integrations.Common
{
    public interface IOrderProcessingFailedEvent
    {
        public Guid CorrelationId { get; }
        public string ErrorMessage { get; }
    }
}
