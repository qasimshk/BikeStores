namespace bs.order.domain.Models
{
    public record AddressDto(
        string Street
        , string PostCode
        , string City
        , string Country);
}
