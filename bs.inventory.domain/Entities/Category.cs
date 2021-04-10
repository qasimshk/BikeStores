using bs.component.sharedkernal.Abstractions;
using bs.component.sharedkernal.Common;
using System.Collections.Generic;
using bs.inventory.domain.Events;

namespace bs.inventory.domain.Entities
{
    public class Category : Entity, IAggregateRoot
    {
        private List<Product> _products;

        protected Category()
        {
            _products = new List<Product>();
        }

        public Category(string name)
        {
            Name = name.Trim();
        }

        public string Name { get; private set; }

        public IReadOnlyCollection<Product> Products => _products;

        public void AddProduct(string name, int modelYear, double listPrice, int brandId, int storeId, int quantity)
        {
            AddDomainEvent(new AddProductEvent(name, modelYear, listPrice, brandId, Id, storeId, quantity));
        }
    }
}
