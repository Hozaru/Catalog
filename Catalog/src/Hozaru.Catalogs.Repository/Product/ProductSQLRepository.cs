using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Catalogs.Catalog.Repositories;
using Hozaru.Database.SQLDataMapper;

namespace Hozaru.Catalogs.Repository.Product
{
    public class ProductSQLRepository : IProductRepository
    {
        readonly string _tableName = "Product";

        public void Insert(Catalog.Aggregates.Product product)
        {
            throw new NotImplementedException();
        }

        public bool IsExist(Guid storeId, Catalog.ValuesObjects.ProductInformation information)
        {
            throw new NotImplementedException();
        }

        public void UpdateInformation(Catalog.Aggregates.Product product)
        {
            throw new NotImplementedException();
        }

        public Catalog.Aggregates.Product Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid id)
        {
            throw new NotImplementedException();
        }

        public void CreateTable()
        {
            if(SQL.IsTableExist(_tableName))
                return;

            SQL.Do(@"CREATE TABLE Product (
                         Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
                         Storeid UNIQUEIDENTIFIER NOT NULL, 
                         ""Information.Code"" VARCHAR(16) NOT NULL,
                         ""Information.Name"" VARCHAR(64) NOT NULL,
                         ""Information.ShortDescription"" VARCHAR(256),
                         ""Information.Description"" VARCHAR(256),
                         ""Status"" int NOT NULL,
                         ""Price.SupplyPrice"" NUMERIC,
                         ""Price.RetailPrice"" NUMERIC,
                         ""Category.Key.Code"" VARCHAR(16) NOT NULL,
                         ""Category.Name"" VARCHAR(64) NOT NULL
                        )")
            .AddParameter("tableName", _tableName)
            .Than
            .Execute();
        }
    }
}
