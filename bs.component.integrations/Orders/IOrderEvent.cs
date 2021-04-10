using bs.component.integrations.Common;
using System;
using System.Collections.Generic;

namespace bs.component.integrations.Orders
{
    public interface IOrderEvent
    {
        public Guid CorrelationId { get; }
        public Guid OrderRef { get; }
        public IAddressEvent DeliveryAddress { get; }
        public IList<IOrderItemsEvent> OrderItems { get; }
    }
}
