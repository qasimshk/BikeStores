﻿using bs.component.sharedkernal.Common;
using System.Collections.Generic;
using System.Linq;

namespace bs.inventory.domain.Entities
{
    public class Product : Entity
    {
        protected Product()
        {
            _stocks = new List<Stock>();
        }

        public Product(string name, int modelYear, double listPrice, int brandId, int categoryId, int storeId, int quantity)
        {
            _brandId = brandId;
            _categoryId = categoryId;
            Name = name.Trim();
            ModelYear = modelYear;
            ListPrice = listPrice;

            _stocks = new List<Stock>
            {
                new(storeId, Id, quantity)
            };
        }

        private readonly int _brandId;
        private readonly int _categoryId;
        private IList<Stock> _stocks;

        public IList<Stock> Stocks => _stocks;
        public int InStock => _stocks.Select(x => x.Quantity()).Sum();
        public string Name { get; private set; }
        public Brand Brand { get; }
        public Category Category { get; }
        public int ModelYear { get; private set; }
        public double ListPrice { get; private set; }

        public void AddStock(int storeId, int quantity)
        {
            _stocks = new List<Stock>
            {
                new(storeId, Id, quantity)
            };
        }
    }
}