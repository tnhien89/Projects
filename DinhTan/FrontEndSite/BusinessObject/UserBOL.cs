using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEndSite.BusinessObject
{
    public class UserBOL : BOL
    {
        public int Id { get; set; }
        public string FirstName_VN { get; set; }
        public string FirstName_EN { get; set; }
        public string LastName_VN { get; set; }
        public string LastName_EN { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public int DepartmentId { get; set; }
        public string Status { get; set; }
        public int MenuId { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public UserBOL()
        { }

        public UserBOL(DataRow row)
            : base(row)
        { }
    }
}
