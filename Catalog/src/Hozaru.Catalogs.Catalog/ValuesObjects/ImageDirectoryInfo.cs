using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Catalogs.Catalog.ValuesObjects
{
    public class ImageDirectoryInfo
    {
        public string Name { get; private set; }
        public string FullName { get; private set; }
        public string RootName { get; private set; }
        public string RootFullName { get; private set; }

        public ImageDirectoryInfo(DirectoryInfo directoryInfo)
        {
            this.Name = directoryInfo.Name;
            this.RootName = directoryInfo.Root.Name;
            this.FullName = directoryInfo.FullName;
            this.RootFullName = directoryInfo.Root.FullName;
        }
    }
}
