using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrontEnd.Attributes
{
    public class DbColumnAttribute : Attribute
    {
        public string Name { get; set; }
    }
}