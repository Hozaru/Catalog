using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Hozaru.Database.PostgreSQLDataMapper.Query
{
    public class PostgreSQLTableExistQuery : PostgreSQLBaseCommand
    {
        private string tableName;
        private PostgreSQLTableExistQuery() { }

        internal PostgreSQLTableExistQuery(string tableName)
        {
            this.tableName = tableName;
        }

        public bool IsExist()
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
                            var statement = "SELECT COUNT(*) FROM information_schema.tables WHERE table_name = @tableName";
                            command.CommandText = statement;
                            command.Parameters.AddWithValue("tableName", tableName.ToLower());
                            var count = Convert.ToInt32(command.ExecuteScalar());
                            return count > 0;
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
