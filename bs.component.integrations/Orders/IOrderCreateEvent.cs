using bs.component.integrations.Common;
using System;
using System.Collections.Generic;

namespace bs.component.integrations.Orders
{
    public interface IOrderCreateEvent
    {
        public Guid OrderRef { get; }
        public int PaymentId { get; }
        public int CustomerId { get; }
        public IAddressEvent DeliveryAddress { get; }
        public List<IOrderItemsEvent> OrderItems { get; }
    }
}
