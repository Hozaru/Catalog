using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Hozaru.Database.DataMapper.Query
{
    public class SQLQuery<T> : SQLBaseCommand
    {
        Func<NpgsqlDataReader, T> _deserializer;
        int _pageSize;
        int _pageIndex;

        private SQLQuery() { }
        internal SQLQuery(string commandText) : base(commandText) { }

        public SQLQuery<T> DeserializeUsing(Func<NpgsqlDataReader, T> deserializer)
        {
            _deserializer = deserializer;
            return this;
        }

        public SQLQuery<T> LimitTo(int limit)
        {
            _pageSize = limit;
            return this;
        }
        
        public SQLQuery<T> OffsetFrom(int offset)
        {
            _pageIndex = offset;
            return this;
        }

        public SQLCondition<SQLQuery<T>> AddParameter(string name, object value)
        {
            if (name.IsNullOrWhiteSpace()) throw new NullReferenceException("Parameter name is null");
            name = name.Replace("@", "");
            return new SQLCondition<SQLQuery<T>>(this, name, value);
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
                using (var connection = new NpgsqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        using (var command = connection.CreateCommand())
                        {
                            var commandNotHaveLimitation = !_commandText.ToLowerInvariant().Contains("limit");
                            var commandNotHaveOffset = !_commandText.ToLowerInvariant().Contains("offset");
                            if (_pageSize > 0 && commandNotHaveLimitation)
                            {
                                _commandText = _commandText.TrimEnd(';');
                                _commandText += " ROWS FETCH NEXT @pageSize ROWS ONLY";
                            }
                            if (_pageIndex > 0 && commandNotHaveOffset)
                            {
                                _commandText = _commandText.TrimEnd(';');
                                _commandText += " OFFSET @pageIndex";
                            }
                            command.CommandText = _commandText;
                            SetParametersToCommand(command);
                            if (commandNotHaveLimitation && _pageSize > 0)
                                command.Parameters.Add(new SqlParameter("@pageSize", _pageSize));
                            if (commandNotHaveOffset && _pageIndex > 0)
                                command.Parameters.Add(new SqlParameter("@pageIndex", _pageIndex));

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
