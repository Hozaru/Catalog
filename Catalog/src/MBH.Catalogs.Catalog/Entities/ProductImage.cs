using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Catalogs.Catalog.Entities
{
    public class ProductImage
    {
        public Guid Id { get; private set; }
        public string Caption { get; private set; }
        public string ImageName { get; private set; }
        public bool IsDefault { get; private set; }

        //private ProductImage(string caption, )
        //{
        //}
    }
}
