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

        public static Price Zero()
        {
            return new Price(0D, 0D);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Price)) return false;
            Price that = (Price)obj;
            return this.SupplyPrice == that.SupplyPrice &&
                   this.RetailPrice == that.RetailPrice;
        }
    }
}
