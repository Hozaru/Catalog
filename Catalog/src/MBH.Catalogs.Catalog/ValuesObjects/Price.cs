using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.Exceptions;

namespace Hozaru.Catalogs.Catalog.ValuesObjects
{
    public class Price
    {
        public double SupplyPrice { get; private set; }
        public double RetailPrice { get; private set; }

        public Price(double supplyPrice, double retailPrice)
        {
            if (supplyPrice > retailPrice)
                throw new InvalidPriceException("Harga beli tidak boleh lebih besar dari harga jual");
            this.SupplyPrice = supplyPrice;
            this.RetailPrice = retailPrice;
        }
    }
}
