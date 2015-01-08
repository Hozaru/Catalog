using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.DataMapper;
using Hozaru.Database.Fixture.Contexts;
using Hozaru.Database.Fixture.Model;
using Npgsql;
using NUnit.Framework;

namespace Hozaru.Database.Fixture
{
    [TestFixture]
    public class TestInsertData : Behave_like_table_created
    {
        static string id = Guid.NewGuid().ToString();
        [Test]
        public void Test_Insert_Data()
        {
            Example test = new Example(id, "Test");
            PostgreSQL.Do("INSERT INTO TableExample (Id, Name) VALUES (@id, @name)").AddParameter("id", id).And("name", test.Name).Than.Execute();
            var result = PostgreSQL.FindAs<Example>("select * from TableExample where id = @id").AddParameter("id", id).Than.DeserializeUsing(ExampleDeserializer).ReturnOne();
            Assert.IsNotNull(result);
        }

        [Test]
        [ExpectedException(typeof(NpgsqlException), UserMessage = "ERROR: 23505: duplicate key value violates unique constraint \"tableexample_pkey\"")]
        public void Test_Insert_Data_with_exist_id()
        {
            Example example = new Example(id, "Test Again");
            PostgreSQL.Do("INSERT INTO TableExample (id, name) VALUES (@id, @name)").AddParameter("id", example.Id).And("name", example.Name).Than.Execute();
        }

        [TestFixtureTearDown]
        public void Remove_Data_Inserted()
        {
            PostgreSQL.Do("DELETE FROM TableExample where id = @id").AddParameter("id", id);
        }
    }
}
