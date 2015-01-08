using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Hozaru.Database.DataMapper.Command
{
    public class SQLCreateDatabase : SQLBaseCommand
    {
        public SQLCreateDatabase()
            : base()
        {
        }

        public void CreateDB()
        {
            try
            {
                var connectionStringSplited = ConnectionString.Split(';').ToList();
                var sectionDatabaseName = connectionStringSplited.FirstOrDefault(i => i.ToUpper().Contains("DATABASE"));
                var sectionUserName = connectionStringSplited.FirstOrDefault(i => i.ToUpper().Contains("UID") || i.ToUpper().Contains("USER ID"));
                connectionStringSplited.RemoveAll(i => i.ToUpper().Contains("DATABASE"));
                var connectionStringForCreateDatabase = string.Join(";", connectionStringSplited);
                var databaseName = sectionDatabaseName.Substring(sectionDatabaseName.IndexOf('=') + 1);
                var userName = sectionUserName.Substring(sectionUserName.IndexOf('=') + 1);
                using (var connection = new NpgsqlConnection(connectionStringForCreateDatabase))
                {
                    connection.Open();
                    using (var commandCheckDB = connection.CreateCommand())
                    {
                        commandCheckDB.CommandText = string.Format("SELECT count(*) FROM pg_database WHERE datname='{0}';", databaseName);
                        var exist = (long) commandCheckDB.ExecuteScalar() > 0;
                        if (!exist)
                        {
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandText = string.Format("CREATE DATABASE {0} WITH OWNER {1} ENCODING='UTF8' CONNECTION LIMIT = -1;", databaseName, userName);
                                command.ExecuteNonQuery();
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
