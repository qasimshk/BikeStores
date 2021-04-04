using bs.component.integrations.Common;
using System;
using System.Collections.Generic;

namespace bs.component.integrations.Orders
{
    public interface IOrder
    {
        public Guid CorrelationId { get; }
        public Guid OrderRef { get; }
        public IAddress DeliveryAddress { get; }
        public IList<IOrderItems> OrderItems { get; }
    }
}
