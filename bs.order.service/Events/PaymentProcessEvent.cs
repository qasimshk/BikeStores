using bs.component.integrations.Customers;
using bs.component.integrations.Payments;
using bs.order.domain.Entities;
using Newtonsoft.Json;
using System;

namespace bs.order.service.Events
{
    public class PaymentProcessEvent : IPaymentProcessEvent
    {
        private readonly OrderState _orderState;

        public PaymentProcessEvent(OrderState orderState, double basketPrice)
        {
            _orderState = orderState;
            Amount = basketPrice;
            CardDetail = JsonConvert.DeserializeObject<CardDetailEvent>(_orderState.JsonCardDetails);
        }

        public Guid CorrelationId => _orderState.CorrelationId;
        public int PaymentType => _orderState.PaymentType;
        public double Amount { get; private set; }
        public int CustomerId => _orderState.CustomerId;
        public int? CardDetailsId => _orderState.CardDetailId;
        public ICardDetailEvent CardDetail { get; }
    }
}
