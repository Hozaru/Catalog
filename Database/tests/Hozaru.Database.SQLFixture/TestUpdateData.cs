using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.SQLDataMapper;
using Hozaru.Database.SQLFixture.Contexts;
using Hozaru.Database.SQLFixture.Model;
using NUnit.Framework;

namespace Hozaru.Database.SQLFixture
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
            SQL.Do("UPDATE TableExample SET name = @name where id = @id").AddParameter("name", dataExample.Name).And("id", dataExample.Id).Than.Execute();
            var result = SQL.FindAs<Example>("select * from TableExample where id = @id").AddParameter("id", dataExample.Id).Than.DeserializeUsing(ExampleDeserializer).ReturnOne();
            Assert.AreEqual(result.Name, "Test 1 2 3");
        }
    }
}
