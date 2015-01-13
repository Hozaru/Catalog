using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.Entities;
using Hozaru.Catalogs.Catalog.ValuesObjects;

namespace Hozaru.Catalogs.Catalog.DomainServices
{
    public interface IImageGenerator
    {
        /// <summary>
        /// Generate image.
        /// Resize to ImageCombinations and save to path {PathFileStorageDirectory}\{StoreId}\{ProductID}\{ImageID}\{CombinationName}
        /// Configuration must have app setting with key PathFileStorageDirectory
        /// </summary>
        /// <returns></returns>
        ImageDirectoryInfo Generate(System.Drawing.Image image, Guid storeId, Guid productId, Guid imageId);
    }
}
