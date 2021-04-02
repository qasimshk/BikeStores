namespace bs.order.domain.Models
{
    public record ConsentDto(
        bool ContactByEmail
        , bool ContactByText
        , bool ContactByCall
        , bool ContactByPost);
}
