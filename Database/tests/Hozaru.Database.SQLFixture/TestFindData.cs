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
    public class TestFindData : Behave_like_data_example_inserted
    {
        [Test]
        public void Find_data()
        {
            var example = SQL.FindAs<Example>("select * from TableExample where id = @id") 
                .AddParameter("id", dataExample.Id)
                .Than
                .DeserializeUsing(ExampleDeserializer)
                .ReturnOne();
            Assert.IsNotNull(example);
        }

        [Test]
        public void Find_data_with_limit()
        {
            var example = SQL.FindAs<Example>("select TOP(1) * from TableExample where id = @id")
                .AddParameter("id", dataExample.Id)
                .Than
                .DeserializeUsing(ExampleDeserializer)
                .ReturnAll();
            Assert.IsNotNull(example);
            Assert.Greater(example.Count, 0);
        }
    }
}
