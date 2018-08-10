using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndSite.BusinessObject
{
    public class ContactBOL : BOL
    {
        public int Id { get; set; }
        public string Name_VN { get; set; }
        public string Name_EN { get; set; }
        public string Address_VN { get; set; }
        public string Address_EN { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Subject_VN { get; set; }
        public string Subject_EN { get; set; }
        public string Content_VN { get; set; }
        public string Content_EN { get; set; }
        public int MenuId { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public ContactBOL()
        { }

        public ContactBOL(DataRow row)
            : base(row)
        { 
            
        }
    }
}
