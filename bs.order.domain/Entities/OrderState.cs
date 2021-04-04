using System;
using Automatonymous;

namespace bs.order.domain.Entities
{
    public class OrderState : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public string CurrentState { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? FailedOn { get; set; }
        public Guid TransactionRef { get; set; }
        public int CustomerId { get; set; }
        public int OrderId { get; set; }
        public int PaymentId { get; set; }
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
