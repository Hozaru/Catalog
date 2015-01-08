using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;

namespace Hozaru.Database.SQLDataMapper.Query
{
    public class SQLQuery<T> : SQLBaseCommand
    {
        Func<SqlDataReader, T> _deserializer;
        int _pageSize;
        int _pageIndex;

        private SQLQuery() { }
        internal SQLQuery(string commandText) : base(commandText) { }

        public SQLQuery<T> DeserializeUsing(Func<SqlDataReader, T> deserializer)
        {
            _deserializer = deserializer;
            return this;
        }

        public SQLiteCondition<SQLQuery<T>> AddParameter(string name, object value)
        {
            if (name.IsNullOrWhiteSpace()) throw new NullReferenceException("Parameter name is null");
            name = name.Replace("@", "");
            return new SQLiteCondition<SQLQuery<T>>(this, name, value);
        }

        public T ReturnOne()
        {
            return ReturnAll().FirstOrDefault();
        }

        public IList<T> ReturnAll()
        {
            try
            {
                if (_deserializer.IsNull())
                    throw new NullReferenceException(string.Format("Deserializer {0} has not set", typeof(T).Name));
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var command = connection.CreateCommand())
                        {
                            command.Transaction = transaction;
                            command.CommandText = _commandText;
                            SetParametersToCommand(command);
                            using (var reader = command.ExecuteReader())
                            {
                                List<T> entities = new List<T>();
                                while (reader.Read())
                                {
                                    var entity = _deserializer.Invoke(reader);
                                    entities.Add(entity);
                                }
                                return entities;
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