using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Database.PostgreSQLDataMapper
{
    public static class ObjectExtension
    {
        public static bool IsNull(this object obj)
        {
            return Object.ReferenceEquals(obj, null);
        }
        public static bool IsNotNull(this object obj)
        {
            return !IsNull(obj);
        }
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return String.IsNullOrWhiteSpace(str);
        }
    }
}
