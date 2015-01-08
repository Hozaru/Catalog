using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hozaru.Catalogs.Catalog.Exceptions
{
    public class CategoryException : Exception
    {
        public CategoryException(string message, params string[] args)
            : base(string.Format(message, args))
        {
        }
    }
}
