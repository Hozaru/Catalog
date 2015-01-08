using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Hozaru.Database.PostgreSQLDataMapper.Query
{
    public class PostgreSQLAgregator : PostgreSQLBaseCommand
    {
        private PostgreSQLAgregator() { }
        internal PostgreSQLAgregator(string commandText) : base(commandText) { }

        public SQLCondition<PostgreSQLAgregator> Where(string name, object value)
        {
            if (name.IsNullOrWhiteSpace()) throw new NullReferenceException("Parameter name is null");
            return new SQLCondition<PostgreSQLAgregator>(this, name, value);
        }

        public T ReturnAs<T>()
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
