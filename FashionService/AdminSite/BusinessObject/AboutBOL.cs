using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminSite.BusinessObject
{
    public class AboutBOL : BOL
    {
        public int Id { get; set; }
        public string Name_VN { get; set; }
        public string Name_EN { get;set; }
        public string ImageLink { get; set; }
        public string Address_VN { get; set; }
        public string Address_EN { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string WebSite { get; set; }
        public string GoogleMaps { get; set; }
        public int MenuId { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public AboutBOL()
        { 
            
        }

        public AboutBOL(DataRow row)
            : base(row)
        { 
            
        }
    }
}
