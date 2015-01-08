using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Hozaru.Database.PostgreSQLDataMapper.Command
{
    public class PostgreSQLBatchExecutor<T> : PostgreSQLBaseCommand
    {
        private PostgreSQLBatchExecutor() { }
        internal PostgreSQLBatchExecutor(string commandText)
            : base(commandText)
        {
        }

        public void ForData(IEnumerable<T> datas, Action<NpgsqlParameterCollection, T> setParameterValue)
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
                            CreateParametersToCommand(command);
                            try
                            {
                                foreach (var item in datas)
                                {
                                    setParameterValue.Invoke(command.Parameters, item);
                                    command.ExecuteNonQuery();
                                }
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
