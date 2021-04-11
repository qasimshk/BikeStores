using bs.component.sharedkernal.Common;
using bs.inventory.domain.Exceptions;

namespace bs.inventory.domain.Entities
{
    public class BasketItem : Entity
    {
        protected BasketItem() { }

        public BasketItem(int productId, int quantity, double amount, int basketId)
        {
            if (quantity == 0 || amount == 0)
            {
                throw new BasketItemDomainException("Invalid quantity or amount value");
            }

            _basketId = basketId;
            _productId = productId;
            Quantity = quantity;
            Amount = amount;
        }

        private readonly int _basketId;
        private readonly int _productId;

        public Basket Basket { get; }
        public double Amount { get; private set; }
        public int Quantity { get; private set; }

        public int GetProductId => _productId;
    }
}
