using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.DataMapper;
using NUnit.Framework;

namespace Hozaru.Database.Fixture
{
    [TestFixture]
    public class TestCreateDatabase : Setup
    {
        [Test]
        public void Create_Database_Test()
        {
            PostgreSQL.CreateDatabase();
        }
    }
}
