using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminSite.BusinessObject
{
    public class Department_SecurityBOL : BOL
    {
        public int DepartmentId { get; set; }
        public int SecurityId { get; set; }
        public string Insert { get; set; }
        public string Update { get; set; }
        public string Delete { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public Department_SecurityBOL()
        { }

        public Department_SecurityBOL(DataRow row)
            : base(row)
        { }
    }
}
