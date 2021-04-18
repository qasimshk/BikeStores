using Automatonymous;
using bs.component.integrations.Basket;
using bs.component.integrations.Common;
using bs.component.integrations.Customers;
using bs.component.integrations.Orders;
using bs.component.integrations.Payments;
using bs.component.integrations.Requests;
using bs.order.domain.Entities;
using bs.order.domain.Enums;
using bs.order.service.Events;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace bs.order.service.Orchestrator
{
    public class OrderStateMachine : MassTransitStateMachine<OrderState>
    {
        public OrderStateMachine()
        {
            Event(() => OrderSubmitEvent, o => o.CorrelateById(x => x.Message.OrderRef));
            Event(() => CustomerCreatedEvent, c => c.CorrelateById(x => x.Message.CorrelationId));
            Event(() => PaymentCreatedEvent, p => p.CorrelateById(x => x.Message.CorrelationId));
            Event(() => OrderCreatedEvent, o => o.CorrelateById(x => x.Message.OrderRef));
            Event(() => OrderProcessingFailedEvent, o => o.CorrelateById(x => x.Message.OrderRef));
            Event(() => BasketStatusEvent, b => b.CorrelateById(x => x.Message.OrderRef));
            Event(() => BasketNotFoundEvent, b => b.CorrelateById(x => x.Message.OrderRef));
            Event(() => PaymentFailedEvent, b => b.CorrelateById(x => x.Message.CorrelationId));
            
            InstanceState(s => s.CurrentState);

            Initially(
                When(OrderSubmitEvent)
                    .Publish(context => new SubmitBasketValidateEvent(context.Data))
                    .ThenAsync(async context =>
                    {
                        // Delay for basket validation to complete
                        await Task.Delay(5000); 

                        context.Instance.CorrelationId = context.Data.OrderRef;
                        context.Instance.OrderRef = context.Data.OrderRef;
                        context.Instance.BasketRef = context.Data.BasketRef;
                        context.Instance.CreatedOn = DateTime.Now.Date;
                        context.Instance.PaymentType = context.Data.Payment.PaymentType;
                        context.Instance.JsonDeliveryAddress = JsonConvert.SerializeObject(context.Data.DeliveryAddress);

                        if (context.Data.Payment.PaymentType == (int)PaymentType.Card)
                        {
                            context.Instance.JsonCardDetails = JsonConvert.SerializeObject(context.Data.Customer.CardDetails);
                        }
                    })
                    .Publish(context => context.Data.Customer)
                    .TransitionTo(ProcessingOrder));

            During(ProcessingOrder,
                When(CustomerCreatedEvent)
                    .Then(context =>
                    {
                        context.Instance.CustomerId = context.Data.CustomerId;

                        if (context.Data.CardDetailId > 0)
                        {
                            context.Instance.CardDetailId = (int)context.Data.CardDetailId;
                        }
                    })
                    .Publish(context => new BasketStatusRequestEvent(context.Instance.BasketRef))
                    .TransitionTo(CustomerCreatedOrUpdated));

            During(PaymentEntryCreated,
                When(OrderCreatedEvent)
                    .Then(context =>
                    {
                        context.Instance.OrderId = context.Data.OrderId;
                    })
                    //.Publish() TODO: send notification orchestrator to notify customer
                    //.Publish() TODO: send inventory orchestrator to create entry in stock out
                    .TransitionTo(OrderSubmitted));

            DuringAny(
                When(OrderProcessingFailedEvent)
                    .Then(context =>
                    {
                        context.Instance.FailedOn = DateTime.Now;
                        context.Instance.ErrorMessage = context.Data.ErrorMessage;
                    })
                    .TransitionTo(OrderProcessingFailed),

                When(BasketStatusEvent)
                    .Then(context =>
                    {
                        context.Instance.JsonOrderItems = context.Data.JsonBasketItems;
                    })
                    .IfElse(context => context.Data.CurrentState == "ItemsInStock",
                    inStock => inStock.Publish(context => new PaymentProcessEvent(context.Instance, context.Data.BasketPrice)),
                    outStock => outStock
                        .Then(context =>
                        {
                            context.Instance.ErrorMessage = "One of the ordered items is out of stock";
                        })
                        .TransitionTo(OrderOnHold)),

                When(PaymentCreatedEvent)
                    .Then(context =>
                    {
                        context.Instance.PaymentId = context.Data.PaymentId;
                        context.Instance.TransactionRef = context.Data.TransactionRef;
                    })
                    .Publish(context => new OrderCreateEvent(context.Instance))
                    .TransitionTo(PaymentEntryCreated),

                When(PaymentFailedEvent)
                    .Then(context =>
                    {
                        context.Instance.PaymentId = context.Data.PaymentId;
                        context.Instance.ErrorMessage = context.Data.ErrorMessage;
                    })
                    .TransitionTo(PaymentFailed),

                When(BasketNotFoundEvent)
                    .TransitionTo(InvalidBasketReference));

            SetCompletedWhenFinalized();
        }

        #region State

        private State ProcessingOrder { get; set; }
        private State CustomerCreatedOrUpdated { get; set; }
        private State PaymentEntryCreated { get; set; }
        private State PaymentFailed { get; set; }
        private State OrderSubmitted { get; set; }
        private State OrderOnHold { get; set; }
        private State InvalidBasketReference { get; set; }
        private State OrderProcessingFailed { get; set; }

        #endregion

        #region Events

        private Event<ICustomerCreatedEvent> CustomerCreatedEvent { get; set; }
        private Event<IOrderProcessingFailedEvent> OrderProcessingFailedEvent { get; set; }
        private Event<IOrderSubmitEvent> OrderSubmitEvent { get; set; }
        private Event<IPaymentCreatedEvent> PaymentCreatedEvent { get; set; }
        private Event<IOrderCreatedEvent> OrderCreatedEvent { get; set; }
        private Event<IBasketStatusEvent> BasketStatusEvent { get; set; }
        private Event<IBasketNotFoundEvent> BasketNotFoundEvent { get; set; }
        private Event<IPaymentFailedEvent> PaymentFailedEvent { get; set; }

        #endregion
    }
}

