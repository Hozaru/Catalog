using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Catalogs.Catalog.Exceptions
{
    public class InvalidPriceException : Exception
    {
        public InvalidPriceException(string message, params string[] args)
            : base(string.Format(message, args))
        {
        }
    }
}
