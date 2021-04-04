using System.Collections.Generic;

namespace bs.order.domain.Models
{
    public record OrderDto(
        AddressDto DeliveryAddress
        , List<OrderItemDto> OrderItemDtos);
}
