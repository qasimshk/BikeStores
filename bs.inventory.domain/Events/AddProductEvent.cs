using MediatR;

namespace bs.inventory.domain.Events
{
    public class AddProductEvent : INotification
    {
        public string Name { get; }
        public int ModelYear { get; }
        public double ListPrice { get; }
        public int BrandId { get; }
        public int CategoryId { get; }
        public int StoreId { get; }
        public int Quantity { get; }

        public AddProductEvent(string name, int modelYear, double listPrice, int brandId,int categoryId ,int storeId, int quantity)
        {
            Name = name;
            ModelYear = modelYear;
            ListPrice = listPrice;
            BrandId = brandId;
            CategoryId = categoryId;
            StoreId = storeId;
            Quantity = quantity;
        }
    }
}
