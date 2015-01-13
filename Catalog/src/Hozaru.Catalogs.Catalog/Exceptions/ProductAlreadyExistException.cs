using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Catalogs.Catalog.Exceptions
{
    public class ProductAlreadyExistException : Exception
    {
        public ProductAlreadyExistException(string code)
            : base(string.Format("Produk dengan kode ({0}) sudah terdaftar", code))
        {
        }
    }
}
