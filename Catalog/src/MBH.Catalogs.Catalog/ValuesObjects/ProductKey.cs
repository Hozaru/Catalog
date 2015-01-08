using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Catalogs.Catalog.ValuesObjects
{
    public class ProductKey
    {
        private Guid id;

        public ProductKey(Guid id)
        {
            this.id = id;
        }

        #region Members

        public Guid Id
        {
            get { return id; }
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj != null
                && obj.GetType() == typeof(ProductKey)
                && this == (ProductKey)obj;
        }

        public override int GetHashCode()
        {
            return this.id.GetHashCode();
        }

        #endregion
    }
}
