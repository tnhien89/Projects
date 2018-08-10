using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd
{
    public class ResultData<T>
    {
        public int Code { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
        public int DbReturnValue { get; set; }

        public ResultData()
        { }
    }
}
