using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNetLibrary.Attributes
{
    public class DbColumnAttribute : Attribute
    {
        public string Name { get; set; }
        public DbColumnAttribute()
        { }

        public DbColumnAttribute(string name)
        {
            Name = name;
        }
    }
}
