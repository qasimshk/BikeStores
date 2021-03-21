using bs.order.domain.Enums;
using System;
using System.Collections.Generic;

namespace bs.order.Tests.Models
{
    public class MockOrder
    {
        public Guid OrderRef { get; set; }
        public OrderStatus Status { get; set; }
        public int PaymentId { get; set; }
        public int CustomerId { get; set; }
        public MockAddress DeliveryAddress { get; set; }
        public List<MockOrderItem> MockOrderItems { get; set; }
    }
}
