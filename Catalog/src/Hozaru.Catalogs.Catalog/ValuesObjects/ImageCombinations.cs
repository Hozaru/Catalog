using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Catalogs.Catalog.ValuesObjects
{
    public class ImageCombinations
    {
        public static ImageCombination MASTER = new ImageCombination("master", 0, 0);
        public static ImageCombination HOME = new ImageCombination("home", 250, 250);
        public static ImageCombination SMALL = new ImageCombination("small", 98, 98);
        public static ImageCombination MEDIUM = new ImageCombination("medium", 125, 125);
        public static ImageCombination LARGE = new ImageCombination("large", 458, 458);
        public static ImageCombination THINKBOX = new ImageCombination("thinkbox", 800, 800);
    }
}
