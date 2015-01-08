using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Catalogs.Catalog.ValuesObjects
{
    public class ProductInformation
    {
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }

        public ProductInformation(string code, string name, string shortDescription, string description)
        {
            if (name.Equals(string.Empty))
                throw new ArgumentException("Nama produk harus diisi.");
            if (code.Equals(string.Empty))
                throw new ArgumentException("Kode produk harus diisi.");

            this.Code = code;
            this.Name = name;
            this.ShortDescription = shortDescription;
            this.Description = description;
        }
    }
}
