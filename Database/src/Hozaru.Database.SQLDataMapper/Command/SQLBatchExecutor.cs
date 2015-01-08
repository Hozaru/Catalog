using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading;

namespace Hozaru.Database.SQLDataMapper.Command
{
    public class SQLBatchExecutor<T> : SQLBaseCommand
    {
        private SQLBatchExecutor() { }
        internal SQLBatchExecutor(string commandText)
            : base(commandText)
        {
        }

        public void ForData(IEnumerable<T> datas, Action<SqlParameterCollection, T> setParameterValue)
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