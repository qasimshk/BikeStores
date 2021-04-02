using bs.order.domain.Enums;
using System;

namespace bs.order.domain.Models
{
    public record OrderDto(
        Guid OrderRef
        , OrderStatus OrderStatus
        , int PaymentId
        , int CustomerId
        , AddressDto DeliveryAddress);
}
