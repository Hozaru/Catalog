using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Hozaru.Database.PostgreSQLDataMapper
{
    public abstract class PostgreSQLBaseCommand
    {
        protected string _commandText;
        readonly protected List<NpgsqlParameter> _parameters = new List<NpgsqlParameter>();

        protected PostgreSQLBaseCommand() { }
        protected PostgreSQLBaseCommand(string commandText)
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

        protected void SetParametersToCommand(NpgsqlCommand command)
        {
            _parameters.ForEach(p => command.Parameters.Add(p));
        }

        protected void CreateParametersToCommand(NpgsqlCommand command)
        {
            var cmdText = command.CommandText;
            cmdText = cmdText.Remove(0, cmdText.IndexOf('@'));
            cmdText = cmdText.Remove(cmdText.LastIndexOf(')'), cmdText.Length - cmdText.LastIndexOf(')'));
            var paramNames = cmdText.Split(',');
            foreach (var pm in paramNames)
            {
                command.Parameters.Add(new NpgsqlParameter(pm.Trim(), System.Data.SqlDbType.VarChar));
            }
        }

        private void AddParameter(NpgsqlParameter parameter)
        {
            _parameters.Add(parameter);
        }

        protected void LogOrSendMailToAdmin(Exception ex)
        {
        }

        public class SQLCondition<T> where T : PostgreSQLBaseCommand
        {
            T _baseCommand;
            internal SQLCondition(T baseCommand, string name, object value)
            {
                _baseCommand = baseCommand;
                _baseCommand.AddParameter(new NpgsqlParameter("@" + name, value));
            }
            public SQLCondition<T> And(string name, object value)
            {
                if (name.IsNullOrWhiteSpace()) throw new NullReferenceException("Parameter name is null");
                _baseCommand.AddParameter(new NpgsqlParameter("@" + name, value));
                return this;
            }
            public T Than { get { return _baseCommand; } }
        }
    }
}
