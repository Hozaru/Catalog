using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.SQLDataMapper;
using NUnit.Framework;

namespace Hozaru.Database.SQLFixture
{
    [TestFixture]
    public class TestCreateDatabase : Setup
    {
        [Test]
        public void Create_Database_Test()
        {
            SQL.CreateDatabase();
        }
    }
}
