using System;
using bs.component.sharedkernal.Common;
using bs.order.domain.Exceptions;

namespace bs.order.domain.Entities
{
    public class OrderItem : Entity
    {
        protected OrderItem() { }

        public OrderItem(Guid productRef, string productName, int quantity, double individualPrice, int orderId)
        {
            if (quantity <= 0)
            {
                throw new OrderingDomainException("Invalid number of quantity");
            }

            _orderId = orderId;
            ProductRef = productRef;
            ProductName = productName;
            Quantity = quantity;
            IndividualPrice = individualPrice;
        }

        private int _orderId;
        public Order Order { get; }
        public Guid ProductRef { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public double IndividualPrice { get; private set; }
        public double TotalPrice => Math.Round(Quantity * IndividualPrice,2);
    }
}
