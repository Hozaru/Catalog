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
    public class TestFindData : Behave_like_data_example_inserted
    {
        [Test]
        public void Find_data()
        {
            var example = PostgreSQL.FindAs<Example>("select * from TableExample where id = @id") 
                .AddParameter("id", dataExample.Id)
                .Than
                .DeserializeUsing(ExampleDeserializer)
                .ReturnOne();
            Assert.IsNotNull(example);
        }

        [Test]
        public void Find_data_with_limit()
        {
            var example = PostgreSQL.FindAs<Example>("select * from TableExample where id = @id")
                .AddParameter("id", dataExample.Id)
                .Than
                .DeserializeUsing(ExampleDeserializer)
                .LimitTo(1)
                .ReturnAll();
            Assert.IsNotNull(example);
            Assert.Greater(example.Count, 0);
        }
    }
}
