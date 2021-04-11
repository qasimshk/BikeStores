using bs.component.sharedkernal.Common;
using bs.inventory.domain.Exceptions;

namespace bs.inventory.domain.Entities
{
    public class Stock : Entity
    {
        protected Stock() { }

        public Stock(int storeId, int productId, int quantity)
        {
            if (quantity == 0)
            {
                throw new StockDomainException("Invalid quantity");
            }

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
