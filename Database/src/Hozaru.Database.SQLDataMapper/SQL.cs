using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Hozaru.Database.SQLDataMapper.Command;
using Hozaru.Database.SQLDataMapper.Query;

namespace Hozaru.Database.SQLDataMapper
{
    public static class SQL
    {
        public static bool IsColumnExists(string tableName, string columnName)
        {
            return GetSchema(tableName).Rows.Cast<DataRow>().Count(r => r["COLUMN_NAME"].Equals(columnName)) > 0;
        }

        public static bool IsColumnNotExists(string tableName, string columnName)
        {
            return IsColumnExists(tableName, columnName) == false;
        }

        public static SQLQuery<T> FindAs<T>(string sql)
        {
            return new SQLQuery<T>(sql);
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
        }

        public static DataTable GetSchema(string tableName)
        {
            if (!IsTableExist(tableName))
                throw new Exception(string.Format("Table {0} not exist", tableName));
            return new SQLExecuteDataReader().GetSchema(tableName);
        }
    }
}