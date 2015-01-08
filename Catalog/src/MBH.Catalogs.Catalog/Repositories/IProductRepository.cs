using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Catalogs.Catalog.Repositories
{
    public interface IProductRepository
    {
        void Insert(Aggregates.Product product);

        bool IsExist(Guid storeId, ValuesObjects.ProductInformation information);

        void UpdateInformation(Aggregates.Product product);

        Aggregates.Product Get(Guid id);

        void Remove(Guid id);
    }
}
