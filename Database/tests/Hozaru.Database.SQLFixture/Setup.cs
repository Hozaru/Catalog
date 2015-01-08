using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.SQLFixture.Model;
using System.Data.SqlClient;

namespace Hozaru.Database.SQLFixture
{
    public class Setup
    {
        protected static Example ExampleDeserializer(SqlDataReader reader)
        {
            var id = reader["Id"].ToString();
            var name = reader["Name"].ToString();
            return new Example(id, name);
        }
    }
}
