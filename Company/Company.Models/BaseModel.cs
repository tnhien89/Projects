using Company.Extensions.Attributes;
using System;

namespace Company.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public string Status { get; set; }

        [IgnoreParam]
        public DateTimeOffset? CreatedAt { get; set; }
        public int CreatedBy { get; set; }

        [IgnoreParam]
        public DateTimeOffset? UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
        [IgnoreParam]
        public string UpdatedByUsername { get; set; }

        public BaseModel()
        { }
    }
}
