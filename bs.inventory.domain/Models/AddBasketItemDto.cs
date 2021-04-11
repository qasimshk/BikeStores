using System;

namespace bs.inventory.domain.Models
{
    public record AddBasketItemDto(
        Guid ProductRef
        , int Quantity);
}
