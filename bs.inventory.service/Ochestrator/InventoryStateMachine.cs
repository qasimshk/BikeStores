using Automatonymous;
using bs.component.integrations.Basket;
using bs.inventory.domain.Entities;
using bs.inventory.service.Events;
using System;
using MassTransit;

namespace bs.inventory.service.Ochestrator
{
    public class InventoryStateMachine : MassTransitStateMachine<InventoryStatus>
    {
        public InventoryStateMachine()
        {
            Event(() => BasketSubmitEvent, b => b.CorrelateById(x => x.Message.BasketRef));
            Event(() => BasketPlacedSuccessfullyEvent, b => b.CorrelateById(x => x.Message.BasketRef));
            Event(() => BasketProcessFailedEvent, b => b.CorrelateById(x => x.Message.BasketRef));
            Event(() => SubmitBasketValidateEvent, b => b.CorrelateById(x => x.Message.BasketRef));
            Event(() => BasketItemsStockCheckResultEvent, b => b.CorrelateById(x => x.Message.BasketRef));

            Event(() => BasketStatusRequestEvent, b =>
            {
                b.CorrelateById(x => x.Message.BasketRef);
                b.OnMissingInstance(m => m.ExecuteAsync(async context =>
                {
                    if (context.RequestId.HasValue)
                    {
                        await context.RespondAsync<IBasketNotFoundEvent>(new
                        {
                            BasketRef = context.Message.BasketRef
                        });
                    }
                }));
            });

            InstanceState(x => x.CurrentState);

            Initially(
                When(BasketSubmitEvent)
                    .Then(context =>
                    {
                        context.Instance.CorrelationId = context.Data.BasketRef;
                        context.Instance.BasketRef = context.Data.BasketRef;
                    })
                    .Publish(context => new CreateBasketEvent(context.Data))
                    .TransitionTo(Processing));

            During(Processing,
                When(BasketPlacedSuccessfullyEvent)
                    .Then(context =>
                    {
                        context.Instance.CreatedOn = DateTime.Now;
                        context.Instance.BasketPrice = context.Data.BasketPrice;
                        context.Instance.JsonBasketItems = context.Data.JsonBasketItems;
                    })
                    .TransitionTo(BasketIsPlaced));

            DuringAny(
                When(BasketSubmitEvent)
                    .Publish(context => new UpdateBasketEvent(context.Data))
                    .TransitionTo(Processing),

            When(BasketProcessFailedEvent)
                .Then(context =>
                {
                    context.Instance.ErrorMessage = context.Data.ErrorMessage;
                    context.Instance.FailedOn = DateTime.Now;
                })
                .TransitionTo(ProcessFailed),

            When(SubmitBasketValidateEvent)
                .Then(context =>
                {
                    context.Instance.BasketRef = context.Data.BasketRef;
                    context.Instance.OrderRef = context.Data.OrderRef;

                })
                .Publish(context => new BasketValidateEvent(context.Data.BasketRef))
                .TransitionTo(Validating),

            When(BasketItemsStockCheckResultEvent)
                .IfElse(context => context.Data.InStock,
                    inStock => inStock.TransitionTo(ItemsInStock),
                    outStock => outStock.TransitionTo(ItemsOutOfStock)),

            When(BasketStatusRequestEvent)
                .RespondAsync(x => x.Init<IBasketStatusEvent>(new BasketStatusEvent
                {
                    OrderRef = x.Instance.OrderRef,
                    BasketPrice = x.Instance.BasketPrice,
                    BasketRef = x.Instance.BasketRef,
                    CurrentState = x.Instance.CurrentState,
                    JsonBasketItems = x.Instance.JsonBasketItems
                })));

            SetCompletedWhenFinalized();
        }

        #region States

        private State Processing { get; set; }
        private State Validating { get; set; }
        private State BasketIsPlaced { get; set; }
        private State ProcessFailed { get; set; }
        private State ItemsInStock { get; set; }
        private State ItemsOutOfStock { get; set; }

        #endregion

        #region Events

        private Event<ISubmitBasketEvent> BasketSubmitEvent { get; set; }
        private Event<IBasketPlacedSuccessfullyEvent> BasketPlacedSuccessfullyEvent { get; set; }
        private Event<IBasketProcessFailedEvent> BasketProcessFailedEvent { get; set; }
        private Event<ISubmitBasketValidateEvent> SubmitBasketValidateEvent { get; set; }
        private Event<IBasketItemsStockCheckResultEvent> BasketItemsStockCheckResultEvent { get; set; }
        private Event<IBasketStatusRequestEvent> BasketStatusRequestEvent { get; set; }

        #endregion
    }
}
