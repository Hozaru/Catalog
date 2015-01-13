using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.DomainServices;
using Hozaru.Catalogs.Catalog.Entities;
using Hozaru.Catalogs.Catalog.ValueObjects;
using Hozaru.Catalogs.Catalog.ValuesObjects;

namespace Hozaru.Catalogs.Catalog.Aggregates
{
    public class Product
    {
        #region Members

        public Guid Id { get; private set; }
        public Guid StoreId { get; private set; }
        public ProductInformation Information { get; private set; }
        public StatusProduct Status { get; private set; }
        public Price Price { get; private set; }
        public Category Category { get; private set; }
        public List<ProductCombination> Combinations { get; private set; }
        public List<ImageProduct> Images { get; private set; }
        public bool HasCombination
        {
            get { return this.Combinations.Count() > 0; }
        }

        #endregion

        #region Constructors

        private Product(Guid storeId)
        {
            this.Id = Guid.NewGuid();
            this.StoreId = storeId;
        }

        private Product(Guid storeId, ProductInformation information, Price price, Category category)
            : this(storeId)
        {
            this.Information = information;
            this.Price = price;
            this.Category = category;
            this.Status = StatusProduct.Active;
            this.Combinations = new List<ProductCombination>();
            this.Images = new List<ImageProduct>();
        }

        #endregion

        public static Product Create(Guid storeId, ProductInformation information, Price price, Category category)
        {
            return new Product(storeId, information, price, category);
        }

        public void Activate()
        {
            this.Status = StatusProduct.Active;
        }

        public void Discontinue()
        {
            this.Status = StatusProduct.Discontinued;
        }

        public void ChangeInformation(ProductInformation information)
        {
            this.Information = information;
        }

        public void ChangePrice(Price price)
        {
            this.Price = price;
        }

        public void MoveCategory(Category category)
        {
            this.Category = category;
        }

        public ProductCombination AddCombination(string barcode, ImpactType impactType, Price impactOnPrice, List<AttributeProduct> attributes)
        {
            var combination = ProductCombination.Create(barcode, impactType, impactOnPrice, this.Price, attributes);
            this.Combinations.Add(combination);
            return combination;
        }

        public void RemoveCombination(Guid combinationId)
        {
            var combination = this.Combinations.FirstOrDefault(i => i.Id.Equals(combinationId));
            if (combination.IsNull())
                throw new NullReferenceException("Combination tidak ditemukan");
            this.Combinations.Remove(combination);
        }

        public ImageProduct AddImage(IImageGenerator imageGenerator, string caption, Image image, Guid imageId)
        {
            ImageProduct imageProduct;
            if (Images.Count == 0)
                imageProduct = new ImageProduct(imageGenerator,imageId, this, caption, image, false);
            else
                imageProduct = new ImageProduct(imageGenerator,imageId, this, caption, image, true);
            this.Images.Add(imageProduct);
            return imageProduct;
        }
    }
}
