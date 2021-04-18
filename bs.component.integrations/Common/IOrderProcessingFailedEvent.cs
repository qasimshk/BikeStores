using System;

namespace bs.component.integrations.Common
{
    public interface IOrderProcessingFailedEvent
    {
        public Guid OrderRef { get; }
        public string ErrorMessage { get; }
    }
}
