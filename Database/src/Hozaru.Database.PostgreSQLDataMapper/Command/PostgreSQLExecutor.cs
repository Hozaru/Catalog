using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Hozaru.Database.PostgreSQLDataMapper.Command
{
    public class PostgreSQLExecutor : PostgreSQLBaseCommand
    {
        private PostgreSQLExecutor() { }
        internal PostgreSQLExecutor(string commandText) : base(commandText) { }

        public SQLCondition<PostgreSQLExecutor> AddParameter(string name, object value)
        {
            if (name.IsNullOrWhiteSpace()) throw new NullReferenceException("Parameter name is null");
            return new SQLCondition<PostgreSQLExecutor>(this, name, value);
        }
        public void Execute()
        {
            try
            {
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.CommandText = _commandText;
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
