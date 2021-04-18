using bs.component.integrations.Common;
using bs.component.integrations.Orders;
using bs.order.domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace bs.order.service.Events
{
    public class OrderCreateEvent : IOrderCreateEvent
    {
        private readonly OrderState _orderState;

        public OrderCreateEvent(OrderState orderState)
        {
            _orderState = orderState;
            DeliveryAddress = JsonConvert.DeserializeObject<AddressEvent>(_orderState.JsonDeliveryAddress);
            OrderItems = new List<IOrderItemsEvent>(JsonConvert.DeserializeObject<List<OrderItemsEvent>>(_orderState.JsonOrderItems));
        }

        public Guid OrderRef => _orderState.OrderRef;
        public int PaymentId => _orderState.PaymentId;
        public int CustomerId => _orderState.CustomerId;
        public IAddressEvent DeliveryAddress { get; }
        public List<IOrderItemsEvent> OrderItems { get; }
    }
}
