using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.Aggregates;
using Hozaru.Catalogs.Catalog.DomainServices;
using Hozaru.Catalogs.Catalog.ValuesObjects;

namespace Hozaru.Catalogs.Catalog.Entities
{
    public class ImageProduct
    {
        public Guid Id { get; private set; }
        public string Caption { get; private set; }
        public ImageDirectoryInfo DirectoryInfo { get; private set; }
        public bool IsDefault { get; private set; }

        private IImageGenerator _imageGenerator;
        private ImageProduct(IImageGenerator imageGenerator, Guid id)
        {
            _imageGenerator = imageGenerator;
            this.Id = id;
        }

        public ImageProduct(IImageGenerator imageGenerator, Guid imageId, Product product, string caption, Image image, bool isDefault)
            : this(imageGenerator, imageId)
        {
            this.Caption = caption;
            this.IsDefault = IsDefault;
            var directoryInfo = _imageGenerator.Generate(image, product.StoreId, product.Id, imageId);
            this.DirectoryInfo = directoryInfo;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ImageProduct)) return false;
            var that = (ImageProduct)obj;
            return that.Id == this.Id &&
                that.Caption == this.Caption;
        }
    }
}
