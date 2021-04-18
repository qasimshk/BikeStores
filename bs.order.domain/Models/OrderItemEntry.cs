using System;

namespace bs.order.domain.Models
{
    public class OrderItemEntry
    {
        public OrderItemEntry(Guid productRef, string productName, int quantity, double price)
        {
            ProductRef = productRef;
            ProductName = productName;
            Quantity = quantity;
            IndividualPrice = price;
        }

        public Guid ProductRef { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public double IndividualPrice { get; private set; }
    }
}
