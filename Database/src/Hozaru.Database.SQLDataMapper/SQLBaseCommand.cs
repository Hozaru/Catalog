using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace Hozaru.Database.SQLDataMapper
{
    public abstract class SQLBaseCommand
    {
        protected string _commandText;
        readonly protected List<SqlParameter> _parameters = new List<SqlParameter>();

        protected SQLBaseCommand() { }
        protected SQLBaseCommand(string commandText)
        {
            _commandText = commandText;
        }

        protected string ConnectionString
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["Hozaru"].ConnectionString;
                return connectionString;
            }
        }

        protected void SetParametersToCommand(SqlCommand command)
        {
            _parameters.ForEach(p => command.Parameters.Add(p));
        }
        protected void CreateParametersToCommand(SqlCommand command)
        {
            var cmdText = command.CommandText;
            cmdText = cmdText.Remove(0, cmdText.IndexOf('@'));
            cmdText = cmdText.Remove(cmdText.LastIndexOf(')'), cmdText.Length - cmdText.LastIndexOf(')'));
            var paramNames = cmdText.Split(',');
            foreach (var pm in paramNames)
            {
                command.Parameters.Add(new SqlParameter(pm.Trim(), System.Data.SqlDbType.VarChar));
            }
        }
        private void AddParameter(SqlParameter parameter)
        {
            _parameters.Add(parameter);
        }

        protected void LogOrSendMailToAdmin(Exception ex)
        {
        }

        public class SQLiteCondition<T> where T : SQLBaseCommand
        {
            T _baseCommand;
            internal SQLiteCondition(T baseCommand, string name, object value)
            {
                _baseCommand = baseCommand;
                _baseCommand.AddParameter(new SqlParameter("@" + name, value));
            }
            public SQLiteCondition<T> And(string name, object value)
            {
                if (name.IsNullOrWhiteSpace()) throw new NullReferenceException("Parameter name is null");
                _baseCommand.AddParameter(new SqlParameter("@" + name, value));
                return this;
            }
            public T Than { get { return _baseCommand; } }
        }
    }
}