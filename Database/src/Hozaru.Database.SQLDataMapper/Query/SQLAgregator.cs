using System;
using System.Data.SqlClient;
using System.Threading;

namespace Hozaru.Database.SQLDataMapper.Query
{
    public class SQLAgregator : SQLBaseCommand
    {
        private SQLAgregator() { }
        internal SQLAgregator(string commandText) : base(commandText) { }

        public SQLiteCondition<SQLAgregator> Where(string name, object value)
        {
            if (name.IsNullOrWhiteSpace()) throw new NullReferenceException("Parameter name is null");
            return new SQLiteCondition<SQLAgregator>(this, name, value);
        }

        public T ReturnAs<T>()
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
                            return (T)Convert.ChangeType(command.ExecuteScalar(), typeof(T));
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