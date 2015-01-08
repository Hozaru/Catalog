using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.Aggregates;

namespace Hozaru.Catalogs.Catalog.Repositories
{
    public interface ICategoryRepository
    {
        Category Get(ValuesObjects.CategoryKey categoryKey);
    }
}
