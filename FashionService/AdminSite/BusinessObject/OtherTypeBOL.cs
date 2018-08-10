using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminSite.BusinessObject
{
    public class OtherTypeBOL : BOL
    {
        public int Id { get; set; }
        public string Name_VN { get; set; }
        public string Name_EN { get; set; }
        public string Description_VN { get; set; }
        public string Description_EN { get; set; }
        public bool DisableEdit { get; set; }
        public int Index { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public OtherTypeBOL()
        { }

        public OtherTypeBOL(DataRow row)
            : base(row)
        { }
    }
}
