using bs.order.domain.Enums;
using System;

namespace bs.order.Tests.Models
{
    public class MockPayment
    {
        public int CustomerId { get; set; }
        public double Amount { get; set; }
        public PaymentType PaymentType { get; set; }
        public Guid PaymentRef { get; set; }
        public int? CardDetailId { get; set; }
    }
}
