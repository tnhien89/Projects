using System;
using System.Collections;
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
        public string Type { get; set; }
        public int ParentId { get; set; }
        public int MenuId { get; set; }
        public int Priority { get; set; }
        public bool Disable { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public NewsBOL()
        { }

        public NewsBOL(DataRow row)
            : base(row)
        { }

        public override System.Collections.Hashtable GetParameters()
        {
            Hashtable hash = base.GetParameters();
            hash.Remove("Type");
            if (string.IsNullOrEmpty(ImageLink))
            {
                hash.Remove("ImageLink");
            }

            if (string.IsNullOrEmpty(Content_VN))
            {
                hash.Remove("Content_VN");
            }

            if (string.IsNullOrEmpty(Content_EN))
            {
                hash.Remove("Content_EN");
            }

            if (ParentId <= 0)
            {
                hash.Remove("ParentId");
            }

            if (MenuId <= 0)
            {
                hash.Remove("MenuId");
            }

            if (InsertDate == DateTime.MinValue)
            {
                hash.Remove("InsertDate");
            }

            if (UpdatedDate == DateTime.MinValue)
            {
                hash.Remove("UpdatedDate");
            }

            hash.Remove("Priority");
            hash.Remove("Disable");

            return hash;
        }
    }
}
