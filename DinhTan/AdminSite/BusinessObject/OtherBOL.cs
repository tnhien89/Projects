using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminSite.BusinessObject
{
    public class OtherBOL : BOL
    {
        public int Id { get; set; }
        public string Name_VN { get; set; }
        public string Name_EN { get; set; }
        public string Link { get; set; }
        public string ImageLink { get; set; }
        public string Description_VN { get; set; }
        public string Description_EN { get; set; }
        public int OtherType { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public OtherBOL()
        { }

        public OtherBOL(DataRow row)
            : base(row)
        { 
           
        }

        public override System.Collections.Hashtable GetParameters()
        {
            Hashtable result = base.GetParameters();

            if (result["ImageLink"] == null ||
                string.IsNullOrEmpty(result["ImageLink"].ToString()))
            {
                result.Remove("ImageLink");
            }

            return result;
        }
    }
}
