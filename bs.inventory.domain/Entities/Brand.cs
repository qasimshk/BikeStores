using bs.component.sharedkernal.Abstractions;
using bs.component.sharedkernal.Common;
using bs.inventory.domain.Events;
using System.Collections.Generic;

namespace bs.inventory.domain.Entities
{
    public class Brand : Entity, IAggregateRoot
    {
        private List<Product> _products;

        protected Brand()
        {
            _products = new List<Product>();
        }

        public Brand(string name)
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
