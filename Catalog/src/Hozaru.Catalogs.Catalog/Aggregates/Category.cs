using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.ValuesObjects;

namespace Hozaru.Catalogs.Catalog.Aggregates
{
    public class Category
    {
        public virtual CategoryKey Key { get; set; }
        public string Name { get; set; }

        private Category(CategoryKey key, string name)
        {
            this.Key = key;
            this.Name = name;
        }

        public static Category Create(CategoryKey key, string name)
        {
            return new Category(key, name);
        }
    }
}
