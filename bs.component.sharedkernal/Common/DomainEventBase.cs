using bs.component.sharedkernal.Abstractions;
using System;

namespace bs.component.sharedkernal.Common
{
    public class DomainEventBase : IDomainEvent
    {
        public DomainEventBase()
        {
            this.OccurredOn = DateTime.Now;
        }

        public DateTime OccurredOn { get; }
    }
}
