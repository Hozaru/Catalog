using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.DataMapper;
using Hozaru.Database.Fixture.Contexts;
using Hozaru.Database.Fixture.Model;
using NUnit.Framework;

namespace Hozaru.Database.Fixture
{
    [TestFixture]
    public class TestUpdateData : Behave_like_data_example_inserted
    {
        [TestFixtureSetUp]
        public void Change_Data_Example()
        {
            dataExample.ChangeName("Test 1 2 3");
        }

        [Test]
        public void Update_data_example()
        {
            PostgreSQL.Do("UPDATE TableExample SET name = @name where id = @id").AddParameter("name", dataExample.Name).And("id", dataExample.Id).Than.Execute();
            var result = PostgreSQL.FindAs<Example>("select * from TableExample where id = @id").AddParameter("id", dataExample.Id).Than.DeserializeUsing(ExampleDeserializer).ReturnOne();
            Assert.AreEqual(result.Name, "Test 1 2 3");
        }
    }
}
