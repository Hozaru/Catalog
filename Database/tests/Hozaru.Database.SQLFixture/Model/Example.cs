using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Database.SQLFixture.Model
{
    public class Example
    {
        public string Id { get; private set; }
        public string Name { get; private set; }

        public Example() { }

        public Example(string id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

        public void ChangeName(string name)
        {
            this.Name = name;
        }
    }
}
