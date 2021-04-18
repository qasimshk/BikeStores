using System;

namespace bs.component.integrations.Orders
{
    public interface IOrderItemsEvent
    {
        public Guid ProductRef { get; }
        public string ProductName { get; }
        public int Quantity { get; }
        public double Price { get; }
    }
}
