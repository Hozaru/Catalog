using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace Hozaru.Database.SQLDataMapper.Query
{
    public class SQLExecuteDataReader : SQLBaseCommand
    {
        internal SQLExecuteDataReader() { }
        internal SQLExecuteDataReader(string commandText) : base(commandText) { }

        public SQLiteCondition<SQLExecuteDataReader> AddParameter(string name, object value)
        {
            if (name.IsNullOrWhiteSpace()) throw new NullReferenceException("Parameter name is null");
            return new SQLiteCondition<SQLExecuteDataReader>(this, name, value);
        }
        public void ReadTo(Action<SqlDataReader> readerAction)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandText = _commandText;
                            SetParametersToCommand(command);
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    readerAction.Invoke(reader);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogOrSendMailToAdmin(ex);
                throw;
            }
        }

        public DataTable GetSchema(string tableName)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = string.Format("SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE table_name = '{0}'", tableName);
                        command.Parameters.Add(new SqlParameter("@tableName", tableName));
                        var schema = new DataTable(tableName);
                        schema.Columns.Add("TABLE_NAME", typeof(String));
                        schema.Columns.Add("DATA_TYPE", typeof(String));
                        schema.Columns.Add("IS_NULLABLE", typeof(String));
                        schema.Columns.Add("COLUMN_NAME", typeof(String));
                        schema.Columns.Add("COLUMN_DEFAULT", typeof(String));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var name = reader["TABLE_NAME"] is DBNull ? "" : (string)reader["TABLE_NAME"];
                                var type = reader["DATA_TYPE"] is DBNull ? "" : (string)reader["DATA_TYPE"];
                                var isNullable = reader["IS_NULLABLE"] is DBNull ? "" : (string)reader["IS_NULLABLE"];
                                var columnName = reader["COLUMN_NAME"] is DBNull ? "" : (string)reader["COLUMN_NAME"];
                                var defaultValue = reader["COLUMN_DEFAULT"] is DBNull ? "" : (string)reader["COLUMN_DEFAULT"];
                                schema.Rows.Add(name, type, isNullable, columnName, defaultValue);
                            }
                        }
                        return schema;
                    }
                }
            }
            catch (Exception ex)
            {
                LogOrSendMailToAdmin(ex);
                throw;
            }
        }
    }
}