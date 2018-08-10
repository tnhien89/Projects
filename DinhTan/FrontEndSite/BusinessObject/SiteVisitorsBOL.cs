using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndSite.BusinessObject
{
    public class SiteVisitorsBOL : BOL
    {
        public int InDay { get; set; }
        public int InMonth { get; set; }
        public int InYear { get; set; }
        public int Total { get; set; }

        public SiteVisitorsBOL()
        { }

        public SiteVisitorsBOL(DataRow row)
            : base(row)
        { }
    }
}
