using System;

namespace bs.inventory.domain.Models
{
    public class JsonBasketItem
    {
        public Guid ProductRef { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
