using System;

namespace bs.component.integrations.Common
{
    public interface IOrderProcessingFailed
    {
        public Guid CorrelationId { get; }
        public string ErrorMessage { get; }
    }
}
