using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Catalogs.Catalog.ValuesObjects
{
    public class CategoryKey
    {
        private string code;

        public CategoryKey(string code)
        {
            AssertCodeNotNullOrWhiteSpace(code);
            this.code = code;
        }

        private void AssertCodeNotNullOrWhiteSpace(string code)
        {
            if (code.IsNullOrWhiteSpace())
                throw new ArgumentException("Code harus diisi");
        }

        #region Members

        public string Code
        {
            get { return code; }
        }

        #endregion

        #region Equality

        public override bool Equals(object obj)
        {
            return obj != null
                && obj.GetType() == typeof(CategoryKey)
                && this == (CategoryKey)obj;
        }

        public override int GetHashCode()
        {
            return this.code.GetHashCode();
        }

        #endregion
    }
}
