using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Hozaru.Database.DataMapper.Query
{
    public class SQLExecuteDataReader : SQLBaseCommand
    {
        private SQLExecuteDataReader() { }
        internal SQLExecuteDataReader(string commandText) : base(commandText) { }

        public SQLCondition<SQLExecuteDataReader> AddParameter(string name, object value)
        {
            if (name.IsNullOrWhiteSpace()) throw new NullReferenceException("Parameter name is null");
            return new SQLCondition<SQLExecuteDataReader>(this, name, value);
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
