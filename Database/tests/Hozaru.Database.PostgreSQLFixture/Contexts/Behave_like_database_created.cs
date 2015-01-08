using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.PostgreSQLDataMapper;
using NUnit.Framework;

namespace Hozaru.Database.PostgreSQLFixture.Contexts
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
