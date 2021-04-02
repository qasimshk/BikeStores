using bs.order.domain.Enums;

namespace bs.order.domain.Models
{
    public record CardDetailsDto(
        int CustomerId
        , CardType CardType
        , string CardHolderName);
}
