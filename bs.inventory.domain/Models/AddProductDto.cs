namespace bs.inventory.domain.Models
{
    public record AddProductDto(
        string Name
        , int BrandId
        , int CategoryId
        , int ModelYear
        , double ListPrice);
}
