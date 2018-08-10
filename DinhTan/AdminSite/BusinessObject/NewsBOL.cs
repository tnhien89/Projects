using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminSite.BusinessObject
{
    public class NewsBOL : BOL
    {
        public int Id { get; set; }
        public string Name_VN { get; set; }
        public string Name_EN { get; set; }
        public string ImageLink { get; set; }
        public string Description_VN { get; set; }
        public string Description_EN { get; set; }
        public string VacancyId { get; set; }
        public string Content_VN { get; set; }
        public string Content_EN { get; set; }
        public int ParentId { get; set; }
        public int MenuId { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public NewsBOL()
        { }

        public NewsBOL(DataRow row)
            : base(row)
        { }
    }
}
