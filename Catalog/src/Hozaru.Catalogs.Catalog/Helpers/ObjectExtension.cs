using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Catalogs.Catalog
{
    public static class ObjectExtension
    {
        public static bool IsNotEmpty(this IEnumerable enumerable)
        {
            if (enumerable == null)
                return false;
            foreach (var item in enumerable)
            {
                return true;
            }
            return false;
        }

        public static bool IsEmpty(this IEnumerable enumerable)
        {
            return !enumerable.IsNotEmpty();
        }

        public static bool IsNull(this object obj)
        {
            return Object.ReferenceEquals(obj, null);
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return String.IsNullOrWhiteSpace(str);
        }

        public static bool IsZero(this double amount)
        {
            return amount == 0;
        }
    }
}
