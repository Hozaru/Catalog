using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Hozaru.Catalogs.Catalog.Aggregates;
using Hozaru.Catalogs.Catalog.Entities;
using Hozaru.Catalogs.Catalog.ValueObjects;
using Hozaru.Catalogs.Catalog.ValuesObjects;
using NUnit.Framework;
using Hozaru.Catalogs.Catalog.DomainServices;
using Moq;

namespace Hozaru.Catalogs.Catalog.Fixtures
{
    [TestFixture]
    public class ProductTest
    {
        private ProductInformation information;
        private Price price;
        private Category category;
        private List<AttributeProduct> defaultAttributes;
        private Product product;
        private Guid storeId;

        [TestFixtureSetUp]
        public void Initialize()
        {
            information = new ProductInformation("001", "Tshirt Hommies", "100% Cotton", "");
            price = new Price(40000, 50000);
            category = Category.Create(new CategoryKey("CTN"), "Cotton");
            storeId = Guid.NewGuid();
            product = Product.Create(storeId, information, price, category);
            defaultAttributes = new List<AttributeProduct>();
            var attribute = new Moq.Mock<AttributeProduct>().Object;
            defaultAttributes.Add(attribute);
        }

        [Test]
        public void Test_Create_Product()
        {
            var product = Product.Create(storeId, information, price, category);
            Assert.IsNotNull(product);
            Assert.AreEqual(product.Status, StatusProduct.Active);
            Assert.AreEqual(product.Information, information);
            Assert.AreEqual(product.Category, category);
            Assert.AreEqual(product.Price, price);
            Assert.IsFalse(product.HasCombination);
        }

        [Test]
        public void Test_Change_Information()
        {
            var information = new ProductInformation("001", "Baju Hommies", "100% Cotton Super", "Bahan Bagus");
            product.ChangeInformation(information);
            Assert.AreEqual(product.Information, information);
        }

        [Test]
        public void Test_Change_Price()
        {
            var price = new Price(30000, 55000);
            product.ChangePrice(price);
            Assert.AreEqual(product.Price, price);
        }

        [Test]
        public void Test_Move_Category()
        {
            var category = Category.Create(new CategoryKey("SPK"), "Spandek");
            product.MoveCategory(category);
            Assert.AreEqual(product.Category, category);
        }

        [Test]
        public void Test_Discontinued_Product()
        {
            product.Discontinue();
            Assert.AreEqual(product.Status, StatusProduct.Discontinued);
        }

        [Test]
        public void Test_Activate_Product()
        {
            product.Activate();
            Assert.AreEqual(product.Status, StatusProduct.Active);
        }

        [Test]
        public void Test_Add_Combination()
        {
            var impactPrice = new Price(0, 5000);
            product.AddCombination("0011", ImpactType.Increase, impactPrice, defaultAttributes);
            Assert.IsTrue(product.HasCombination);
            Assert.IsNotNull(product.Combinations.Find(i => i.Barcode == "0011"));
            Assert.AreEqual(product.Combinations.Find(i => i.Barcode == "0011").Price.RetailPrice, 55000);
            Assert.AreEqual(product.Combinations.Find(i => i.Barcode == "0011").Price.SupplyPrice, 40000);
        }

        [Test]
        public void Test_Remove_Combination()
        {
            var impactPrice = new Price(0, 5000);
            var combination = product.AddCombination("0011", ImpactType.Increase, impactPrice, defaultAttributes);
            product.RemoveCombination(combination.Id);
            Assert.IsFalse(product.Combinations.Contains(combination));
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Test_Remove_Combination_Not_Found()
        {
            product.RemoveCombination(Guid.Empty);
        }

        [Test]
        public void Test_Add_Image()
        {
            var caption = "Image Test";
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"images-src\picture1.jpg");
            var image = Image.FromFile(filePath);
            var mockImageGenerator = new Mock<IImageGenerator>();
            var imagePath = string.Format(@"Assets\Images\Products\{0}\CTN\{1}", product.StoreId, product.Id);
            var imageId = Guid.NewGuid();

            ImageDirectoryInfo directoryInfo = new ImageDirectoryInfo(new DirectoryInfo(imagePath));

            mockImageGenerator.Setup(i => i.Generate(image, product.StoreId, product.Id, imageId)).Returns(directoryInfo);

            var imageProduct = product.AddImage(mockImageGenerator.Object, caption, image, imageId);
            Assert.Greater(product.Images.Count(), 0);
            Assert.AreEqual(product.Images.FirstOrDefault(i => i.Id == imageProduct.Id).Caption, caption);
            Assert.AreEqual(product.Images.FirstOrDefault(i => i.Id == imageProduct.Id).DirectoryInfo, directoryInfo);
        }
    }
}
