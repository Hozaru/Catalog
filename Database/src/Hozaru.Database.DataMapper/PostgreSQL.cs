using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.DataMapper.Command;
using Hozaru.Database.DataMapper.Query;

namespace Hozaru.Database.DataMapper
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

        public static SQLExecuteDataReader Find(string commandText)
        {
            return new SQLExecuteDataReader(commandText);
        }

        public static SQLExecutor Do(string commandText)
        {
            return new SQLExecutor(commandText);
        }

        public static SQLBatchExecutor<T> DoBatch<T>(string commandText)
        {
            return new SQLBatchExecutor<T>(commandText);
        }

        public static SQLAgregator AgregateOf(string sql)
        {
            return new SQLAgregator(sql);
        }

        public static bool IsTableExist(string tableName)
        {
            return new SQLTableExistQuery(tableName).IsExist();
        }

        public static bool IsExists(string sql)
        {
            var count = AgregateOf(sql).ReturnAs<long>();
            return count > 0;
        }

        public static void CreateDatabase()
        {
            new SQLCreateDatabase().CreateDB();
            //string NpgsqlCommand = string.Format("Create Database {0}", databaseName);
            //Do(NpgsqlCommand).Execute();
            //new SQLCreateDatabase().CreateDB();
        }
    }
}
