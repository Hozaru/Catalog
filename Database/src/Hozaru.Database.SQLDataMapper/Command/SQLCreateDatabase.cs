using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace Hozaru.Database.SQLDataMapper.Command
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
                using (var connection = new SqlConnection(connectionStringForCreateDatabase))
                {
                    connection.Open();
                    using (var commandCheckDB = connection.CreateCommand())
                    {
                        commandCheckDB.CommandText = string.Format("SELECT count(*) FROM master.dbo.sysdatabases WHERE name='{0}';", databaseName);
                        var exist = (int)commandCheckDB.ExecuteScalar() > 0;
                        if (!exist)
                        {
                            using (var command = connection.CreateCommand())
                            {
                                command.CommandText = string.Format("CREATE DATABASE {0};", databaseName, userName);
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
