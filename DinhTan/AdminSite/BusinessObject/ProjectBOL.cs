using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminSite.BusinessObject
{
    public class ProjectBOL : BOL
    {
        public int Id { get; set; }
        public string Name_VN { get; set; }
        public string Name_EN { get; set; }
        public string ImageLink { get; set; }
        public string Description_VN { get; set; }
        public string Description_EN { get; set; }
        public string Address_VN { get; set; }
        public string Address_EN { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Content_VN { get; set; }
        public string Content_EN { get; set; }
        public int ParentId { get; set; }
        public int RootLevel { get; set; }
        public int MenuId { get; set; }
        public string IsRedirectUrl { get; set; }
        public string RedirectUrl { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public ProjectBOL()
        { }

        public ProjectBOL(DataRow row)
            : base(row)
        { }

        public override System.Collections.Hashtable GetParameters()
        {
            var result = base.GetParameters();
            if (this.StartDate == DateTime.MinValue)
            {
                result.Remove("StartDate");
            }

            if (EndDate == DateTime.MinValue)
            {
                result.Remove("EndDate");
            }

            return result;
        }
    }
}
