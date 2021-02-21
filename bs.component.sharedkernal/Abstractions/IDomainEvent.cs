using MediatR;
using System;

namespace bs.component.sharedkernal.Abstractions
{
    public interface IDomainEvent : INotification
    {
        DateTime OccurredOn { get; }
    }
}
