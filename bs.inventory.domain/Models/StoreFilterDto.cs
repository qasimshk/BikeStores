namespace bs.inventory.domain.Models
{
    public record StoreFilterDto(
        string Name
        , int Phone
        , string Street
        , string City
        , string PostCode
        , string Country);
}
