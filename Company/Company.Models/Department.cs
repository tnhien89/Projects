using System;

namespace Company.Models
{
    public class Department : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ParentId { get; set; }
    }
}
