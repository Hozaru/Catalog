using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hozaru.Catalogs.Catalog.Aggregates;
using Hozaru.Catalogs.Catalog.Exceptions;
using Hozaru.Catalogs.Catalog.Repositories;
using Hozaru.Catalogs.Catalog.ValuesObjects;

namespace Hozaru.Catalogs.Catalog.DomainServices
{
    public class ProductService
    {
        private IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }

        public void ChangeInformation(Guid id, Guid storeId, ProductInformation information)
        {
            Product product = _productRepository.Get(id);
            AssertProductNotAlreadyExist(storeId, information, product);
            product.ChangeInformation(information);
            _productRepository.UpdateInformation(product);
        }

        private void AssertProductNotAlreadyExist(Guid storeId, ProductInformation information, Product product)
        {
            if (product.Information.Code != information.Code)
                if (_productRepository.IsExist(storeId, information))
                    throw new ProductAlreadyExistException(information.Code);
        }
    }
}
