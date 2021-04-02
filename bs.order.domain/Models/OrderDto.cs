using bs.order.domain.Enums;
using System;
using System.Collections.Generic;

namespace bs.order.domain.Models
{
    public record OrderDto(
        Guid OrderRef
        , OrderStatus OrderStatus
        , AddressDto DeliveryAddress
        , List<OrderItemDto> OrderItemDtos);
}
