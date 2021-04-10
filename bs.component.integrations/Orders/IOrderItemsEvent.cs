using System;

namespace bs.component.integrations.Orders
{
    public interface IOrderItemsEvent
    {
        public Guid ProductRef { get; }
        public string ProductName { get; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
