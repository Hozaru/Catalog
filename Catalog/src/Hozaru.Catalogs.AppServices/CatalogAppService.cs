using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.Factories;
using Hozaru.Catalogs.Catalog.Repositories;
using Hozaru.Catalogs.Catalog.ValuesObjects;

namespace Hozaru.Catalogs.AppServices
{
    public class CatalogAppService
    {
        private ProductFactory _productFactory;
        public CatalogAppService(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            _productFactory = new ProductFactory(productRepository, categoryRepository);
        }

        public Guid CreateProduct(Guid storeId, ProductInformation information, Price price, CategoryKey categoryKey)
        {
            var productId = _productFactory.Create(storeId, information, price, categoryKey);
            return productId;
        }
    }
}
