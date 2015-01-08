using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;

namespace Hozaru.Database.SQLDataMapper.Query
{
    public class SQLTableExistQuery : SQLBaseCommand
    {
        private string tableName;
        private SQLTableExistQuery() { }

        internal SQLTableExistQuery(string tableName)
        {
            this.tableName = tableName;
        }

        public bool IsExist()
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var command = connection.CreateCommand())
                    {
                        var statement = "SELECT COUNT(*) FROM information_schema.tables WHERE table_name = @tableName";
                        command.CommandText = statement;
                        command.Parameters.AddWithValue("tableName", tableName);
                        var count = Convert.ToInt32(command.ExecuteScalar());
                        return count > 0;
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
