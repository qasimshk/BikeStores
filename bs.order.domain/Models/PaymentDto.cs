using bs.order.domain.Enums;

namespace bs.order.domain.Models
{
    public record PaymentDto(
        double Amount
        , PaymentType PaymentType);
}
