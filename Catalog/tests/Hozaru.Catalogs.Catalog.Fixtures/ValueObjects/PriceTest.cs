using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.Exceptions;
using Hozaru.Catalogs.Catalog.ValuesObjects;
using NUnit.Framework;

namespace Hozaru.Catalogs.Catalog.Fixtures
{
    [TestFixture]
    public class PriceTest
    {
        [Test]
        public void Test_Create_Price()
        {
            var price = new Price(2000D, 3000D);
            Assert.IsNotNull(price);
            Assert.AreEqual(price.SupplyPrice, 2000D);
            Assert.AreEqual(price.RetailPrice, 3000D);
        }

        [Test]
        public void Test_Create_Zero_Price()
        {
            var price = Price.Zero();
            Assert.IsNotNull(price);
            Assert.AreEqual(price.SupplyPrice, 0D);
            Assert.AreEqual(price.RetailPrice, 0D);
        }

        [Test]
        [ExpectedException(typeof(InvalidPriceException))]
        public void Test_Create_Price_But_SupplyPrice_Greather_Than_RetailPrice()
        {
            new Price(3000D, 2000D);
        }
    }
}
