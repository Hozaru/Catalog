using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Database.SQLDataMapper
{
    static class ReferenceObjectExtension
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
