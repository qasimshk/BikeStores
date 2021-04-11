namespace bs.inventory.domain.Models
{
    public record AddCategoryDto(
        string Name
        , AddProductDto? Product);
}
