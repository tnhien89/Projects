using Company.Extensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Models
{
    public class UploadFileInfo
    {
        public long Id { get; set; }
        public string FileName { get; set; }
        public string FileData { get; set; }
        public string FileType { get; set; }
        public string Description { get; set; }
        [IgnoreParam]
        public DateTimeOffset? CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        [IgnoreParam]
        public DateTimeOffset? UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
        //---
        [DbColumn("Method")]
        public string SaveMethod { get; set; }
    }
}
