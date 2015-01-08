using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hozaru.Database.PostgreSQLDataMapper;
using NUnit.Framework;

namespace Hozaru.Database.PostgreSQLFixture
{
    [TestFixture]
    public class TestCreateTable : TestCreateDatabase
    {
        static string tableName = "TableExample";
        [Test]
        public void Test_Create_Table()
        {
            if (!PostgreSQL.IsTableExist(tableName))
            {
                PostgreSQL.Do("CREATE TABLE TableExample (id VARCHAR(40) PRIMARY KEY, name VARCHAR(120))").Execute();
            }
        }

        [TestFixtureTearDown]
        public void Delete_Table_Created()
        {
            if (PostgreSQL.IsTableExist(tableName))
            {
                PostgreSQL.Do("DROP TABLE TableExample").Execute();
            }
        }
    }
}
