using System;
using System.Data.SqlClient;
using System.Threading;

namespace Hozaru.Database.SQLDataMapper.Query
{
    public class SQLExecuteDataReader : SQLBaseCommand
    {
        private SQLExecuteDataReader() { }
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
    }
}