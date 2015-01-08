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
    public class TestDeleteData : Behave_like_data_example_inserted
    {
        [Test]
        public void Test_delete_data()
        {
            SQL.Do("DELETE FROM TableExample where id = @id").AddParameter("id", dataExample.Id).Than.Execute();
            var result = SQL.FindAs<Example>("select * from TableExample where id = @id").AddParameter("id", dataExample.Id).Than.DeserializeUsing(ExampleDeserializer).ReturnOne();
            Assert.IsNull(result);
        }
    }
}
