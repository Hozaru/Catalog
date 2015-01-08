using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.DataMapper;
using NUnit.Framework;

namespace Hozaru.Database.Fixture.Contexts
{
    [TestFixture]
    public class Behave_like_database_created : Setup
    {
        [TestFixtureSetUp]
        public void Create_Database_Test()
        {
            PostgreSQL.CreateDatabase();
        }
    }
}
