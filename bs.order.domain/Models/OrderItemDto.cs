using System;

namespace bs.order.domain.Models
{
    public record OrderItemDto(
        Guid ProductRef
        , string ProductName
        , int Quantity
        , double Price);
}
