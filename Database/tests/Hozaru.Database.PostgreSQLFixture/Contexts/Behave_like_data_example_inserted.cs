using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.PostgreSQLDataMapper;
using Hozaru.Database.PostgreSQLFixture.Model;
using NUnit.Framework;

namespace Hozaru.Database.PostgreSQLFixture.Contexts
{
    [TestFixture]
    public class Behave_like_data_example_inserted : Behave_like_table_created
    {
        protected static Example dataExample;

        [TestFixtureSetUp]
        public void Insert_Data_Example()
        {
            var id = Guid.NewGuid().ToString();
            dataExample = new Example(id, "Test");
            PostgreSQL.Do("INSERT INTO TableExample (id, name) VALUES (@id, @name)").AddParameter("id", id).And("name", dataExample.Name).Than.Execute();
        }

        [TestFixtureTearDown]
        public void Remove_Data_example_inserted()
        {
            PostgreSQL.Do("DELETE FROM Example where id = @id").AddParameter("id", dataExample.Id);
        }
    }
}
