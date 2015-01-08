using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog;
using Hozaru.Catalogs.Catalog.ValuesObjects;
using Hozaru.Catalogs.Catalog.Aggregates;
using Hozaru.Catalogs.Catalog.Exceptions;
using Hozaru.Catalogs.Catalog.ValueObjects;

namespace Hozaru.Catalogs.Catalog.Entities
{
    public class ProductCombination
    {
        public Guid Id { get; private set; }
        public string Barcode { get; private set; }
        public ImpactType ImpactType { get; private set; }
        public Price ImpactOnPrice { get; private set; }
        public Price Price { get; private set; }
        public List<AttributeProduct> Attributes { get; private set; }

        #region Constructors

        private ProductCombination()
        {
            this.Id = Guid.NewGuid();
        }

        private ProductCombination(string barcode, ImpactType impactType, Price impactOnPrice, Price basePrice, List<AttributeProduct> attributes)
            : this()
        {
            AssertAttributesNotEmpty(attributes);
            AssertBarcodeNotNullOrWhiteSpace(barcode);
            this.Barcode = barcode;
            this.ImpactType = impactType;
            this.ImpactOnPrice = impactOnPrice;
            this.Attributes = attributes;
            this.Price = calculatePrice(basePrice, impactOnPrice, impactType);
        }

        #endregion

        public static ProductCombination Create(string barcode, ImpactType impactType, Price impactOnPrice, Price basePrice, List<AttributeProduct> attributes)
        {
            return new ProductCombination(barcode, impactType, impactOnPrice, basePrice, attributes);
        }

        private Price calculatePrice(Price basePrice, Price impactOnPrice, ImpactType impactType)
        {
            double supplyPrice = 0;
            double retailPrice = 0;
            if (impactOnPrice.SupplyPrice.IsZero())
                supplyPrice = basePrice.SupplyPrice;
            switch (impactType)
            {
                case ValuesObjects.ImpactType.Replace:
                    retailPrice = impactOnPrice.RetailPrice;
                    break;
                case ValuesObjects.ImpactType.Increase:
                    retailPrice = basePrice.RetailPrice + impactOnPrice.RetailPrice;
                    break;
                case ValuesObjects.ImpactType.Reduction:
                    retailPrice = basePrice.RetailPrice - impactOnPrice.RetailPrice;
                    break;
                default:
                    throw new InvalidImpactTypeException();
            };
            var price = new Price(supplyPrice, retailPrice);
            return price;
        }

        private void AssertAttributesNotEmpty(List<AttributeProduct> attributes)
        {
            if (attributes.IsEmpty())
                throw new NullReferenceException("Attribute harus diisi");
        }

        private void AssertBarcodeNotNullOrWhiteSpace(string barcode)
        {
            if (barcode.IsNullOrWhiteSpace())
                throw new ArgumentNullException("Barcode harus diisi");
        }
    }
}
