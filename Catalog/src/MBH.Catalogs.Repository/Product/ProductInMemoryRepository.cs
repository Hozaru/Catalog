using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hozaru.Catalogs.Catalog.Aggregates;
using Hozaru.Catalogs.Catalog.Repositories;
using Hozaru.Catalogs.Catalog.ValuesObjects;

namespace Hozaru.Catalogs.Repository
{
    public class ProductInMemoryRepository : IProductRepository
    {
        private List<Product> _products;
        public ProductInMemoryRepository()
        {
            this._products = new List<Product>();
        }

        public void Insert(Product product)
        {
            this._products.Add(product);
        }

        public bool IsExist(Guid storeId, ProductInformation information)
        {
            return this._products.Count(i => i.Information.Code.Equals(information.Code) && i.StoreId.Equals(storeId)) > 0;
        }

        public void UpdateInformation(Product product)
        {
            _products.RemoveAll(i => i.Id == product.Id);
            _products.Add(product);
        }

        public Product Get(Guid id)
        {
            return _products.FirstOrDefault(i => i.Id == id);
        }

        public void Remove(Guid id)
        {
            _products.RemoveAll(i => i.Id == id);
        }
    }
}
