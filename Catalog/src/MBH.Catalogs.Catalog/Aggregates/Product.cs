using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public List<ProductImage> Images { get; private set; }
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
            this.Images = new List<ProductImage>();
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

        public void AddImage(string caption, string name, Image image)
        {
            //check already exist image name
            //check isdefault
            //save image to folder
            throw new NotImplementedException();
            ProductImage img = new ProductImage();
            this.Images.Add(img);
        }

        //public static Image ScaleImage(Image image, int maxWidth, int maxHeight)
        //{
        //    var ratioX = (double)maxWidth / image.Width;
        //    var ratioY = (double)maxHeight / image.Height;
        //    var ratio = Math.Min(ratioX, ratioY);

        //    var newWidth = (int)(image.Width * ratio);
        //    var newHeight = (int)(image.Height * ratio);

        //    var newImage = new Bitmap(newWidth, newHeight);
        //    Graphics.FromImage(newImage).DrawImage(image, 0, 0, newWidth, newHeight);
        //    return newImage;
        //}
    }
}
