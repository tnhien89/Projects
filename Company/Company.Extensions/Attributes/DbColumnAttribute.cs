using System;

namespace Company.Extensions.Attributes
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
