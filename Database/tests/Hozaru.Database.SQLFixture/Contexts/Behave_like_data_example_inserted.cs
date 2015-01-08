using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.SQLDataMapper;
using Hozaru.Database.SQLFixture.Model;
using NUnit.Framework;

namespace Hozaru.Database.SQLFixture.Contexts
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
            SQL.Do("INSERT INTO TableExample (id, name) VALUES (@id, @name)").AddParameter("id", id).And("name", dataExample.Name).Than.Execute();
        }

        [TestFixtureTearDown]
        public void Remove_Data_example_inserted()
        {
            SQL.Do("DELETE FROM Example where id = @id").AddParameter("id", dataExample.Id);
        }
    }
}
