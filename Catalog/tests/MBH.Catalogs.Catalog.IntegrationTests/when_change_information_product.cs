using System;
using Hozaru.Catalogs.Catalog.DomainServices;
using Hozaru.Catalogs.Catalog.Exceptions;
using Hozaru.Catalogs.Catalog.IntegrationTests.Contexts;
using Hozaru.Catalogs.Catalog.ValuesObjects;
using NUnit.Framework;

namespace Hozaru.Catalogs.Catalog.IntegrationTests
{
    [TestFixture]
    public class when_change_information_product : behave_like_product_created
    {
        private ProductService _productService;
        [TestFixtureSetUp]
        public void Initialiaze()
        {
            _productService = new ProductService(ProductRepository);
        }

        [Test]
        public void Test_Change_Information_Product()
        {
            var information = new ProductInformation("001", "Tshirt Hommies", "100% Cotton", "100% Cotton");
            _productService.ChangeInformation(ProductId_1, StoreId, information);
            var product = ProductRepository.Get(ProductId_1);
            Assert.AreEqual(product.Information.Code, "001");
            Assert.AreEqual(product.Information.Description, "100% Cotton");
            Assert.AreEqual(product.Information.ShortDescription, "100% Cotton");
            Assert.AreEqual(product.Information.Name, "Tshirt Hommies");
        }

        [Test]
        [ExpectedException(typeof(ProductAlreadyExistException))]
        public void Test_Change_Information_Product_With_Code_Already_Exist()
        {
            var information = new ProductInformation("002", "Tshirt Hommies", "100% Cotton", "100% Cotton");
            _productService.ChangeInformation(ProductId_1, StoreId, information);
        }
    }
}
