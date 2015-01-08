using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.DataMapper;
using Hozaru.Database.Fixture.Contexts;
using NUnit.Framework;

namespace Hozaru.Database.Fixture
{
    [TestFixture]
    public class TestCountRow : Behave_like_data_example_inserted
    {
        [Test]
        public void Test_Count_Row()
        {
            var result = PostgreSQL.AgregateOf("SELECT Count(*) FROM TableExample").ReturnAs<long>();
            Assert.Greater(result, 0);
        }
    }
}
