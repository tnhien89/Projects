using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastDeploy.Utilities.Attributes
{
    public class DbColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public DbColumnAttribute(string name)
        {
            Name = name;
        }
    }
}
