using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminSite.BusinessObject
{
    public class RedirectUrlBOL : BOL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public RedirectUrlBOL()
        { }

        public RedirectUrlBOL(DataRow row)
            : base(row)
        { }
    }
}
