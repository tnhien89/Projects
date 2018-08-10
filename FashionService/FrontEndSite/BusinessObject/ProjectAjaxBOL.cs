using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndSite.BusinessObject
{
    public class ProjectAjaxBOL
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageLink { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }

        public ProjectAjaxBOL() 
        { }
    }
}
