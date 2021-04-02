using System;
using bs.order.domain.Enums;

namespace bs.order.domain.Models
{
    public record CardDetailsDto(
        CardType CardType
        , string CardHolderName
        , DateTime Expiration
        , int SecurityNumber
        , string CardNumber);
}
