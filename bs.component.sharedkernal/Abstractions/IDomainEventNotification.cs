using MediatR;

namespace bs.component.sharedkernal.Abstractions
{
    public interface IDomainEventNotification<out TEventType> : INotification
    {
        TEventType DomainEvent { get; }
    }
}
