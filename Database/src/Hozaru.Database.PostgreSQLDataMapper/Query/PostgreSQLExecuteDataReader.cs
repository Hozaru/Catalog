using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Hozaru.Database.PostgreSQLDataMapper.Query
{
    public class PostgreSQLExecuteDataReader : PostgreSQLBaseCommand
    {
        private PostgreSQLExecuteDataReader() { }
        internal PostgreSQLExecuteDataReader(string commandText) : base(commandText) { }

        public SQLCondition<PostgreSQLExecuteDataReader> AddParameter(string name, object value)
        {
            if (name.IsNullOrWhiteSpace()) throw new NullReferenceException("Parameter name is null");
            return new SQLCondition<PostgreSQLExecuteDataReader>(this, name, value);
        }
        public void ReadTo(Action<NpgsqlDataReader> readerAction)
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
