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
    public class TestCreateTable : TestCreateDatabase
    {
        static string tableName = "TableExample";
        [Test]
        public void Test_Create_Table()
        {
            if (!SQL.IsTableExist(tableName))
            {
                SQL.Do("CREATE TABLE TableExample (id VARCHAR(40) PRIMARY KEY, name VARCHAR(120))").Execute();
            }
        }

        [TestFixtureTearDown]
        public void Delete_Table_Created()
        {
            if (SQL.IsTableExist(tableName))
            {
                SQL.Do("DROP TABLE TableExample").Execute();
            }
        }
    }
}
