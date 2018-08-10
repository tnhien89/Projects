using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndSite.BusinessObject
{
    public class MultipleDeleteErrorBOL : BOL
    {
        public int Id { get; set; }
        public int ErrorCode { get; set; }

        public MultipleDeleteErrorBOL()
        { }

        public MultipleDeleteErrorBOL(DataRow row)
            : base(row)
        { }
    }
}
