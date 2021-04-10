using Automatonymous;
using System;

namespace bs.inventory.domain.Entities
{
    public class InventoryStatus : SagaStateMachineInstance
    {
        public Guid CorrelationId { get; set; }
        public Guid BasketRef { get; set; }
        public string CurrentState { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? FailedOn { get; set; }
        public string ErrorMessage { get; set; }
    }
}
