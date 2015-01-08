using System;
using Hozaru.Catalogs.Catalog.Aggregates;
using Hozaru.Catalogs.Catalog.Exceptions;
using Hozaru.Catalogs.Catalog.Factories;
using Hozaru.Catalogs.Catalog.Repositories;
using Hozaru.Catalogs.Catalog.ValuesObjects;
using Hozaru.Catalogs.Repository;
using NUnit.Framework;

namespace Hozaru.Catalogs.Catalog.IntegrationTests
{
    [TestFixture]
    public class when_create_product
    {
        private ProductFactory _productFactory;
        private IProductRepository _productRepository;
        private ICategoryRepository _categoryRepository;
        private CategoryKey _categoryKey;
        private Guid _storeId;
        private Guid _productId;

        [TestFixtureSetUp]
        public void Initialize()
        {
            this._productRepository = new ProductInMemoryRepository();
            this._categoryKey = new CategoryKey("001");
            var categoryRepositoryMock = new Moq.Mock<ICategoryRepository>();
            categoryRepositoryMock.Setup(i => i.Get(_categoryKey)).Returns(Category.Create(_categoryKey, "T-Shirt"));
            this._categoryRepository = categoryRepositoryMock.Object;
            this._productFactory = new ProductFactory(_productRepository, _categoryRepository);
            _storeId = Guid.NewGuid();
        }

        [Test]
        public void Test_Create_Product()
        {
            var information = new ProductInformation("001", "Tshirt Hommies", "100% Cotton", "");
            var price = new Price(40000, 50000);
            _productId = _productFactory.Create(_storeId, information, price, _categoryKey);
            Assert.IsTrue(_productRepository.IsExist(_storeId, information));
        }

        [Test]
        [ExpectedException(typeof(ProductAlreadyExistException))]
        public void Test_Create_With_Same_Code()
        {
            var information = new ProductInformation("001", "Tshirt Hommies", "100% Cotton", "");
            var price = new Price(40000, 50000);
            var productId = _productFactory.Create(_storeId, information, price, _categoryKey);
        }

        [TestFixtureTearDown]
        public void CleanUpData()
        {
            _productRepository.Remove(_productId); 
        }
    }
}
