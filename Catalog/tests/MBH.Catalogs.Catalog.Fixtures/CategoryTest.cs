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
    public class CategoryTest
    {
        [Test]
        public void Test_Create_Category_Key()
        {
            var key = new CategoryKey("CTN");
            Assert.IsNotNull(key);
            Assert.AreEqual(key.Code, "CTN");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Create_Category_Key_String_Empty()
        {
            new CategoryKey("");
        }

        [Test]
        public void Test_Create_Category()
        {
            CategoryKey key = new CategoryKey("CTN");
            var category = Category.Create(key, "Cotton");
            Assert.IsNotNull(category);
            Assert.AreEqual(category.Key.Code, "CTN");
            Assert.AreEqual(category.Name, "Cotton");
        }
    }
}
