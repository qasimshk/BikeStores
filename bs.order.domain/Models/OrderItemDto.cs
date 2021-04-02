using System;

namespace bs.order.domain.Models
{
    public record OrderItemDto(
        int OrderId
        , Guid ProductRef
        , string ProductName
        , int Quantity
        , double Price);
}
