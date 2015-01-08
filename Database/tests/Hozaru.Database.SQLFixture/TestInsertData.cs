using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    public class TestInsertData : Behave_like_table_created
    {
        static string id = Guid.NewGuid().ToString();
        [Test]
        public void Test_Insert_Data()
        {
            Example test = new Example(id, "Test");
            SQL.Do("INSERT INTO TableExample (Id, Name) VALUES (@id, @name)").AddParameter("id", id).And("name", test.Name).Than.Execute();
            var result = SQL.FindAs<Example>("select * from TableExample where id = @id").AddParameter("id", id).Than.DeserializeUsing(ExampleDeserializer).ReturnOne();
            Assert.IsNotNull(result);
        }

        [Test]
        [ExpectedException(typeof(SqlException), UserMessage = "ERROR: 23505: duplicate key value violates unique constraint \"tableexample_pkey\"")]
        public void Test_Insert_Data_with_exist_id()
        {
            Example example = new Example(id, "Test Again");
            SQL.Do("INSERT INTO TableExample (id, name) VALUES (@id, @name)").AddParameter("id", example.Id).And("name", example.Name).Than.Execute();
        }

        [TestFixtureTearDown]
        public void Remove_Data_Inserted()
        {
            SQL.Do("DELETE FROM TableExample where id = @id").AddParameter("id", id);
        }
    }
}
