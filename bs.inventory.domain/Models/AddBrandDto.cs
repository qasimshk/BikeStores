namespace bs.inventory.domain.Models
{
    public record AddBrandDto(
        string Name
        , AddProductDto? Product);
}
