using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.PostgreSQLDataMapper.Command;
using Hozaru.Database.PostgreSQLDataMapper.Query;

namespace Hozaru.Database.PostgreSQLDataMapper
{
    public static class PostgreSQL
    {
        public static bool IsColumnExists(this DataTable schema, string columnName)
        {
            return schema.Rows.Cast<DataRow>().Count(r => r["name"].Equals(columnName)) > 0;
        }

        public static bool IsColumnNotExists(this DataTable schema, string columnName)
        {
            return schema.IsColumnExists(columnName) == false;
        }

        public static SQLQuery<T> FindAs<T>(string commandText)
        {
            return new SQLQuery<T>(commandText);
        }

        public static PostgreSQLExecuteDataReader Find(string commandText)
        {
            return new PostgreSQLExecuteDataReader(commandText);
        }

        public static PostgreSQLExecutor Do(string commandText)
        {
            return new PostgreSQLExecutor(commandText);
        }

        public static PostgreSQLBatchExecutor<T> DoBatch<T>(string commandText)
        {
            return new PostgreSQLBatchExecutor<T>(commandText);
        }

        public static PostgreSQLAgregator AgregateOf(string sql)
        {
            return new PostgreSQLAgregator(sql);
        }

        public static bool IsTableExist(string tableName)
        {
            return new PostgreSQLTableExistQuery(tableName).IsExist();
        }

        public static bool IsExists(string sql)
        {
            var count = AgregateOf(sql).ReturnAs<long>();
            return count > 0;
        }

        public static void CreateDatabase()
        {
            new PostgreSQLCreateDatabase().CreateDB();
        }
    }
}
