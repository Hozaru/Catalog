using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.Fixture.Model;
using Npgsql;

namespace Hozaru.Database.Fixture
{
    public class Setup
    {
        protected static Example ExampleDeserializer(NpgsqlDataReader reader)
        {
            var id = reader["Id"].ToString();
            var name = reader["Name"].ToString();
            return new Example(id, name);
        }
    }
}
