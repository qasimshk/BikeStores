using System;

namespace bs.inventory.domain.Models
{
    public record AddBasketDto(
        Guid BasketRef
        , AddBasketItemDto BasketItems);
}
