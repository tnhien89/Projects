using Company.Extensions.Attributes;
using System;

namespace Company.Models
{
    public class Option
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        [IgnoreParam]
        public DateTimeOffset? CreatedAt { get; set; }
        public int CreatedBy { get; set; }

        [IgnoreParam]
        public DateTimeOffset? UpdatedAt { get; set; }

        public Option()
        { }

        public Option(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public Option(string name, string value, string description)
        {
            Name = name;
            Value = value;
            Description = description;
        }
    }
}
