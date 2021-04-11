using System;

namespace bs.inventory.domain.Models
{
    public record AddBasketItemDto(
        int ProductId
        , int Quantity);
}
