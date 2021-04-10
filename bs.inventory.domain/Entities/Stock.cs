using bs.component.sharedkernal.Common;

namespace bs.inventory.domain.Entities
{
    public class Stock : Entity
    {
        protected Stock() { }

        public Stock(int storeId, int productId, int quantity)
        {
            _storeId = storeId;
            _productId = productId;
            _stockIn = quantity;
            _stockOut = 0;
        }

        private readonly int _storeId;
        private readonly int _productId;
        private readonly int _stockIn;
        private readonly int _stockOut;

        public Store Store { get; }
        public Product Product { get; }

        public int Quantity()
        {
            return _stockIn - _stockOut;
        }
    }
}
