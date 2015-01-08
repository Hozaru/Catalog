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
        public CategoryKey Key { get; private set; }
        public string Name { get; private set; }

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
