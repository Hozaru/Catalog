using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.ValuesObjects;

namespace Hozaru.Catalogs.Catalog.DomainServices.Impl
{
    public class ImageLocalMachineGenerator : IImageGenerator
    {
        public ImageDirectoryInfo Generate(System.Drawing.Image image, Guid storeId, Guid productId, Guid imageId)
        {
            string pathDirectoryProduct;
            bool useBaseDirectory = Convert.ToBoolean(ConfigurationManager.AppSettings["FileStorageUseBaseDirectory"]);
            if (useBaseDirectory)
            {
                pathDirectoryProduct = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"Images\{0}\Products\{1}\{2}", storeId, productId, imageId));
            }
            else
            {
                var pathFileStorageDirectory = ConfigurationManager.AppSettings["PathFileStorageDirectory"];
                if (pathFileStorageDirectory == null || pathFileStorageDirectory.Equals(string.Empty))
                    throw new Exception("File configuration must have App Setting with key PathFileStorageDirectory");

                pathDirectoryProduct = string.Format(@"Images\{0}\{1}\Products\{2}\{3}", pathFileStorageDirectory, storeId, productId, imageId);
            }

            var directoryProductInfo = new DirectoryInfo(pathDirectoryProduct);

            if (!Directory.Exists(pathDirectoryProduct))
                Directory.CreateDirectory(pathDirectoryProduct);

            var imageCombinations = new List<ImageCombination>() { ImageCombinations.MASTER, ImageCombinations.HOME, ImageCombinations.SMALL, ImageCombinations.MEDIUM, ImageCombinations.LARGE, ImageCombinations.THINKBOX };
            foreach (var combination in imageCombinations)
            {
                Bitmap bitmap = ImageResizer.Resize(image, combination);
                var fileName = Path.Combine(directoryProductInfo.FullName, string.Format("{0}.jpg", combination.Name));
                bitmap.Save(fileName);
            }

            return new ImageDirectoryInfo(directoryProductInfo);
        }
    }
}
