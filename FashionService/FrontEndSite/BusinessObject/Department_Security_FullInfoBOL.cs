using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndSite.BusinessObject
{
    public class Department_Security_FullInfoBOL : Department_SecurityBOL
    {
        public string Name_VN { get; set; }
        public string Name_EN { get; set; }
        public string Description_VN { get; set; }
        public string Description_EN { get; set; }

        public Department_Security_FullInfoBOL()
            : base()
        { }

        public Department_Security_FullInfoBOL(DataRow row)
            : base(row)
        { }
    }
}
