using Automatonymous;
using bs.component.integrations.Common;
using bs.component.integrations.Customers;
using bs.component.integrations.Orders;
using bs.component.integrations.Payments;
using bs.component.integrations.Requests;
using bs.order.domain.Entities;
using bs.order.domain.Models;
using System;

namespace bs.order.service.Workflow
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            Event(() => CustomerCreatedEvent, c => c.CorrelateById(x => x.Message.CorrelationId));
            Event(() => PaymentCreatedEvent, p => p.CorrelateById(x => x.Message.CorrelationId));
            Event(() => OrderCreatedEvent, o => o.CorrelateById(x => x.Message.CorrelationId));

            InstanceState(s => s.CurrentState);

            Initially(
                When(OrderSubmitEvent)
                    .Then(context =>
                    {
                        context.Instance.TransactionRef = context.Data.Payment.PaymentRef;
                    })
                    .TransitionTo(ProcessingOrder)
                    .Publish(context => new CreateCustomer(context.Data.Customer)));

            During(ProcessingOrder,
                When(CustomerCreatedEvent)
                    .TransitionTo(CustomerAddedOrUpdated));

            DuringAny(
                When(OrderProcessingFailedEvent)
                    .Then(context =>
                    {
                        context.Instance.FailedOn = DateTime.Now;
                        context.Instance.ErrorMessage = context.Data.ErrorMessage;
                    })
                    .TransitionTo(OrderProcessingFailed));
        }

        #region State

        public State ProcessingOrder { get; private set; }
        public State CustomerAddedOrUpdated { get; private set; }
        public State PaymentEntryCreated { get; private set; }
        public State OrderSubmitted { get; private set; }
        public State OrderProcessingFailed { get; private set; }

        #endregion

        #region Events

        public  Event<IOrderProcessingFailed> OrderProcessingFailedEvent { get; private set; }
        public Event<IOrderSubmit> OrderSubmitEvent { get; private set; }
        public Event<ICustomerCreated> CustomerCreatedEvent { get; private set; }
        public Event<IPaymentCreated> PaymentCreatedEvent { get; private set; }
        public Event<IOrderCreated> OrderCreatedEvent { get; private set; }

        #endregion
    }
}
