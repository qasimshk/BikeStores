using Automatonymous;
using bs.component.integrations.Common;
using bs.component.integrations.Customers;
using bs.component.integrations.Orders;
using bs.component.integrations.Payments;
using bs.component.integrations.Requests;
using bs.order.domain.Entities;
using System;

namespace bs.order.service.Workflow
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            Event(() => OrderSubmitEvent, c => c.CorrelateById(x => x.Message.CorrelationId));
            Event(() => CustomerCreatedEvent, c => c.CorrelateById(x => x.Message.CorrelationId));
            Event(() => PaymentCreatedEvent, p => p.CorrelateById(x => x.Message.CorrelationId));
            Event(() => OrderCreatedEvent, o => o.CorrelateById(x => x.Message.CorrelationId));

            InstanceState(s => s.CurrentState);

            Initially(
                When(OrderSubmitEvent)
                    .Then(context =>
                    {
                        context.Instance.CorrelationId = context.Data.CorrelationId;
                        context.Instance.CreatedOn = DateTime.Now.Date;
                        context.Instance.TransactionRef = Guid.NewGuid(); //context.Data.Payment.PaymentRef;
                    })
                    .Publish(context => context.Data.Customer)
                    .TransitionTo(ProcessingOrder));

            During(ProcessingOrder,
                When(CustomerCreatedEvent)
                    .Then(context =>
                    {
                        context.Instance.CustomerId = context.Data.CustomerId;
                    })
                    .TransitionTo(CustomerAddedOrUpdated));

            DuringAny(
                When(OrderProcessingFailedEvent)
                    .Then(context =>
                    {
                        context.Instance.FailedOn = DateTime.Now;
                        context.Instance.ErrorMessage = context.Data.ErrorMessage;
                    })
                    .TransitionTo(OrderProcessingFailed));

            SetCompletedWhenFinalized();
        }

        #region State

        private State ProcessingOrder { get; set; }
        private State CustomerAddedOrUpdated { get; set; }
        private State PaymentEntryCreated { get; set; }
        private State OrderSubmitted { get; set; }
        private State OrderProcessingFailed { get; set; }

        #endregion

        #region Events

        private Event<IOrderProcessingFailedEvent> OrderProcessingFailedEvent { get; set; }
        private Event<IOrderSubmitEvent> OrderSubmitEvent { get; set; }
        private Event<ICustomerCreatedEvent> CustomerCreatedEvent { get; set; }
        private Event<IPaymentCreatedEvent> PaymentCreatedEvent { get; set; }
        private Event<IOrderCreatedEvent> OrderCreatedEvent { get; set; }

        #endregion
    }
}
