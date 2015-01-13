using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.Aggregates;
using Hozaru.Catalogs.Catalog.ValuesObjects;
using NUnit.Framework;

namespace Hozaru.Catalogs.Catalog.Fixtures
{
    [TestFixture]
    public class ProductInformationTest
    {
        [Test]
        public void Test_Create_Product_Information()
        {
            var information = new ProductInformation("001", "TShirt Hommies", "100% Cotton", "");
            Assert.AreEqual(information.Code, "001");
            Assert.AreEqual(information.Name, "TShirt Hommies");
            Assert.AreEqual(information.ShortDescription, "100% Cotton");
            Assert.AreEqual(information.Description, "");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Create_Product_Information_With_No_Code()
        {
            new ProductInformation("", "", "100% Cotton", "");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Create_Product_Information_With_No_Name()
        {
            new ProductInformation("", "", "100% Cotton", "");
        }
    }
}
