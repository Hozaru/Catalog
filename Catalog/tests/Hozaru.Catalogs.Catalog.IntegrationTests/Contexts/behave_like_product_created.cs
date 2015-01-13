//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Hozaru.Catalogs.Catalog.Aggregates;
//using Hozaru.Catalogs.Catalog.Factories;
//using Hozaru.Catalogs.Catalog.Repositories;
//using Hozaru.Catalogs.Catalog.ValuesObjects;
//using Hozaru.Catalogs.Repository;
//using NUnit.Framework;

//namespace Hozaru.Catalogs.Catalog.IntegrationTests.Contexts
//{
//    [TestFixture]
//    public class behave_like_product_created
//    {
//        protected Guid ProductId_1;
//        protected Guid ProductId_2;
//        protected Guid StoreId;
//        protected IProductRepository ProductRepository;
//        private ProductFactory _productFactory;
//        private ICategoryRepository _categoryRepository;
//        private CategoryKey _categoryKey;

//        [TestFixtureSetUp]
//        public void Initialize()
//        {
//            this.ProductRepository = new ProductInMemoryRepository();
//            this._categoryKey = new CategoryKey("001");
//            var categoryRepositoryMock = new Moq.Mock<ICategoryRepository>();
//            categoryRepositoryMock.Setup(i => i.Get(_categoryKey)).Returns(Category.Create(_categoryKey, "T-Shirt"));
//            this._categoryRepository = categoryRepositoryMock.Object;
//            this._productFactory = new ProductFactory(ProductRepository, _categoryRepository);


//            StoreId = Guid.NewGuid();
//            var information = new ProductInformation("001", "Tshirt Hommies", "100% Cotton", "");
//            var information2 = new ProductInformation("002", "Tshirt LCC", "100% Cotton", "");
//            var price = new Price(40000, 50000);
//            ProductId_1 = _productFactory.Create(StoreId, information, price, _categoryKey);
//            ProductId_2 = _productFactory.Create(StoreId, information2, price, _categoryKey);
//        }

//        [TestFixtureTearDown]
//        public void CleanUpData()
//        {
//            ProductRepository.Remove(ProductId_1);
//            ProductRepository.Remove(ProductId_2);
//        }
//    }
//}
