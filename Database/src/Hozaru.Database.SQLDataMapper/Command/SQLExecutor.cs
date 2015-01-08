using System;
using System.Data.SqlClient;
using System.Threading;

namespace Hozaru.Database.SQLDataMapper.Command
{
    public class SQLExecutor : SQLBaseCommand
    {
        private SQLExecutor() { }
        internal SQLExecutor(string commandText) : base(commandText) { }

        public SQLiteCondition<SQLExecutor> AddParameter(string name, object value)
        {
            if (name.IsNullOrWhiteSpace()) throw new NullReferenceException("Parameter name is null");
            return new SQLiteCondition<SQLExecutor>(this, name, value);
        }
        public void Execute()
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
                            command.Transaction = transaction;
                            SetParametersToCommand(command);
                            try
                            {
                                command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                            catch
                            {
                                transaction.Rollback();
                                throw;
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