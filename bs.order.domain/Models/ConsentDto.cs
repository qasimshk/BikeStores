namespace bs.order.domain.Models
{
    public record ConsentDto(
        int CustomerId
        , bool ContactByEmail
        , bool ContactByText
        , bool ContactByCall
        , bool ContactByPost);
}
