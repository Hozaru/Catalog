using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.PostgreSQLDataMapper;
using Hozaru.Database.PostgreSQLFixture.Contexts;
using Hozaru.Database.PostgreSQLFixture.Model;
using NUnit.Framework;

namespace Hozaru.Database.PostgreSQLFixture
{
    [TestFixture]
    public class TestDeleteData : Behave_like_data_example_inserted
    {
        [Test]
        public void Test_delete_data()
        {
            PostgreSQL.Do("DELETE FROM TableExample where id = @id").AddParameter("id", dataExample.Id).Than.Execute();
            var result = PostgreSQL.FindAs<Example>("select * from TableExample where id = @id").AddParameter("id", dataExample.Id).Than.DeserializeUsing(ExampleDeserializer).ReturnOne();
            Assert.IsNull(result);
        }
    }
}
