using System;

namespace bs.order.Tests.Models
{
    public class MockOrderItem
    {
        public Guid ProductRef { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double IndividualPrice { get; set; }
        public int OrderId { get; set; }
    }
}
