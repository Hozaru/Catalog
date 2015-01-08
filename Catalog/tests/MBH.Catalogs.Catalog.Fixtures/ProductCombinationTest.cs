using System;
using System.Collections.Generic;
using Hozaru.Catalogs.Catalog.Entities;
using Hozaru.Catalogs.Catalog.Exceptions;
using Hozaru.Catalogs.Catalog.ValueObjects;
using Hozaru.Catalogs.Catalog.ValuesObjects;
using NUnit.Framework;

namespace Hozaru.Catalogs.Catalog.Fixtures
{
    [TestFixture]
    public class ProductCombinationTest
    {
        List<AttributeProduct> defaultAttributes;

        [TestFixtureSetUp]
        public void Initialize()
        {
            defaultAttributes = new List<AttributeProduct>();
            var attribute = new Moq.Mock<AttributeProduct>().Object;
            defaultAttributes.Add(attribute);
        }

        [Test]
        [ExpectedException(typeof(NullReferenceException))]
        public void Test_Create_Product_Combination()
        {
            var basePrice = new Price(40000, 50000);
            var impactPrice = new Price(0, 55000);
            ProductCombination.Create("0011", ImpactType.Replace, impactPrice, basePrice, null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_Create_Product_Combination_With_No_Barcode()
        {
            var basePrice = new Price(40000, 50000);
            var impactPrice = new Price(0, 55000);
            var attributes = new List<AttributeProduct>();
            var attribute = new Moq.Mock<AttributeProduct>().Object;
            attributes.Add(attribute);
            ProductCombination.Create("", ImpactType.Replace, impactPrice, basePrice, attributes);
        }

        [Test]
        public void Test_Create_Product_Combination_With_No_Attributes()
        {
            var basePrice = new Price(40000, 50000);
            var impactPrice = new Price(0, 55000);
            var attributes = new List<AttributeProduct>();
            var attribute = new Moq.Mock<AttributeProduct>().Object;
            attributes.Add(attribute);

            var combination = ProductCombination.Create("0011", ImpactType.Replace, impactPrice, basePrice, attributes);
            Assert.IsNotNull(combination);
            Assert.AreEqual(combination.Barcode, "0011");
            Assert.AreEqual(combination.ImpactOnPrice.RetailPrice, 55000);
            Assert.AreEqual(combination.ImpactType, ValuesObjects.ImpactType.Replace);
            Assert.AreEqual(combination.Price.RetailPrice, 55000);
            Assert.AreEqual(combination.Price.SupplyPrice, 40000);
        }

        [Test]
        public void Test_Create_Product_Combination_With_Impact_Type_Increase()
        {
            var basePrice = new Price(40000, 50000);
            var impactPrice = new Price(0, 5000);
            var combination = ProductCombination.Create("0011", ImpactType.Increase, impactPrice, basePrice, defaultAttributes);
            Assert.AreEqual(combination.ImpactType, ValuesObjects.ImpactType.Increase);
            Assert.AreEqual(combination.Price.RetailPrice, 55000);
            Assert.AreEqual(combination.Price.SupplyPrice, 40000);
        }

        [Test]
        public void Test_Create_Product_Combination_With_Impact_Type_Reduction()
        {
            var basePrice = new Price(40000, 50000);
            var impactPrice = new Price(0, 5000);
            var combination = ProductCombination.Create("0011", ImpactType.Reduction, impactPrice, basePrice, defaultAttributes);
            Assert.AreEqual(combination.ImpactType, ValuesObjects.ImpactType.Reduction);
            Assert.AreEqual(combination.Price.RetailPrice, 45000);
            Assert.AreEqual(combination.Price.SupplyPrice, 40000);
        }

        [Test]
        public void Test_Create_Product_Combination_With_Impact_Type_Replace()
        {
            var basePrice = new Price(40000, 50000);
            var impactPrice = new Price(0, 55000);
            var combination = ProductCombination.Create("0011", ImpactType.Replace, impactPrice, basePrice, defaultAttributes);
            Assert.AreEqual(combination.ImpactType, ValuesObjects.ImpactType.Replace);
            Assert.AreEqual(combination.Price.RetailPrice, 55000);
            Assert.AreEqual(combination.Price.SupplyPrice, 40000);
        }

        [Test]
        [ExpectedException(typeof(InvalidImpactTypeException))]
        public void Test_Create_Product_Combination_With_Invalid_Impact_Type()
        {
            var basePrice = new Price(40000, 50000);
            var impactPrice = new Price(0, 5000);
            ProductCombination.Create("0011", (ImpactType)3, impactPrice, basePrice, defaultAttributes);
        }
    }
}
