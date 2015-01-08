using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.Aggregates;
using Hozaru.Catalogs.Catalog.Exceptions;
using Hozaru.Catalogs.Catalog.Repositories;
using Hozaru.Catalogs.Catalog.ValuesObjects;

namespace Hozaru.Catalogs.Catalog.Factories
{
    public class ProductFactory
    {
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        public ProductFactory(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this._productRepository = productRepository;
            this._categoryRepository = categoryRepository;
        }

        public Guid Create(Guid storeId, ProductInformation information, Price price, CategoryKey categoryKey)
        {
            AssertProductNotAlreadyExist(storeId, information);
            var category = _categoryRepository.Get(categoryKey);
            AssertCategoryExist(category);
            var product = Product.Create(storeId, information, price, category);
            _productRepository.Insert(product);
            return product.Id;
        }

        private void AssertCategoryExist(Category category)
        {
            if (category.IsNull())
                throw new CategoryException("Kategory Produk dengan kode ({0}) tidak ditemukan", category.Key.Code);
        }

        private void AssertProductNotAlreadyExist(Guid storeId, ProductInformation information)
        {
            if (_productRepository.IsExist(storeId, information))
                throw new ProductAlreadyExistException(information.Code);
        }
    }
}
