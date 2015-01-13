using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.SQLDataMapper;
using Hozaru.Database.SQLFixture.Contexts;
using NUnit.Framework;

namespace Hozaru.Database.PostgreSQLFixture
{
    [TestFixture]
    public class TestGetSchemaSQL : Behave_like_table_created
    {
        [Test]
        public void Test_Get_Schema()
        {
            var schema = SQL.GetSchema("tableexample");
            Assert.IsNotNull(schema);
        }

        [Test]
        [ExpectedException(typeof(Exception), UserMessage = "Table tableexample1 not exist")]
        public void Test_Get_Schema_With_Not_Exist_Table()
        {
            var schema = SQL.GetSchema("tableexample1");
        }
    }
}
