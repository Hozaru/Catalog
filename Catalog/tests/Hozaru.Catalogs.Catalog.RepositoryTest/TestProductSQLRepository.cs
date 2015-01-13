using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.Aggregates;
using Hozaru.Catalogs.Catalog.Repositories;
using Hozaru.Catalogs.Catalog.ValuesObjects;
using Hozaru.Catalogs.Repository.Product;
using Hozaru.Database.SQLDataMapper;
using NUnit.Framework;

namespace Hozaru.Catalogs.Catalog.RepositoryTest
{
    [TestFixture]
    public class TestProductSQLRepository
    {
        private IProductRepository _productRepository;
        readonly string _tableName = "Product";

        [TestFixtureSetUp]
        public void Setup()
        {
            _productRepository = new ProductSQLRepository();
        }

        [Test]
        public void Test_Create_Table_Product_To_database_SQL()
        {
            _productRepository.CreateTable();
            Assert.IsTrue(SQL.IsColumnExists(_tableName, "Id"));
            Assert.IsTrue(SQL.IsColumnExists(_tableName, "Storeid"));
            Assert.IsTrue(SQL.IsColumnExists(_tableName, "Information.Code"));
            Assert.IsTrue(SQL.IsColumnExists(_tableName, "Information.Name"));
            Assert.IsTrue(SQL.IsColumnExists(_tableName, "Information.ShortDescription"));
            Assert.IsTrue(SQL.IsColumnExists(_tableName, "Information.Description"));
            Assert.IsTrue(SQL.IsColumnExists(_tableName, "Status"));
            Assert.IsTrue(SQL.IsColumnExists(_tableName, "Price.SupplyPrice"));
            Assert.IsTrue(SQL.IsColumnExists(_tableName, "Price.RetailPrice"));
            Assert.IsTrue(SQL.IsColumnExists(_tableName, "Category.Key.Code"));
            Assert.IsTrue(SQL.IsColumnExists(_tableName, "Category.Name"));
        }

        //[Test]
        //public void Test_Insert_Product_To_database()
        //{
        //    var storeId = Guid.NewGuid();
        //    var productInfo = new ProductInformation("123456", "Item A", "", "");
        //    var price = Price.Zero();
        //    var categoryKey = new CategoryKey("General");
        //    var category = Category.Create(categoryKey, "General");
        //    var product = Product.Create(storeId, productInfo, price, category);

        //    _productRepository.Insert(product);
        //}
    }
}
