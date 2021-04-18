using System;
using Automatonymous;

namespace bs.order.domain.Entities
{
    public class OrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public Guid OrderRef { get; set; }
        public Guid BasketRef { get; set; }
        public string CurrentState { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? FailedOn { get; set; }
        public Guid TransactionRef { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int PaymentId { get; set; }
        public int CardDetailId { get; set; }
        public string ErrorMessage { get; set; }
        public int PaymentType { get; set; }
        public string JsonOrderItems { get; set; }
        public string JsonCardDetails { get; set; }
        public string JsonDeliveryAddress { get; set; }
    }
}
