using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminSite.BusinessObject
{
    public class ResultBOL<T>
    {
        public int Code { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
        public int DbReturnValue { get; set; }

        public ResultBOL()
        { }
    }
}
