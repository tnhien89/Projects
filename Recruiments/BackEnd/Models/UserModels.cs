using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace BackEnd.Models
{
    public class UserProfile : BaseObject
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        private DateTime _dob;
        public DateTime? DateOfBirth {
            get {
                return _dob;
            }
            set {
                if (value != null && value.HasValue)
                {
                    _dob = value.Value;
                    DateOfBirthString = value.Value.ToString("MM/dd/yyyy");
                }
                else
                {
                    _dob = DateTime.MinValue;
                }
            }
        }
        public string DateOfBirthString { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string IdentityCardNumber { get; set; }
        public string Description_EN { get;set; }
        public string Description_VN { get; set; }
        public string Status { get; set; }
        public int InsertedBy { get; set; }
        public DateTime? InsertedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public UserProfile()
        { }

        public UserProfile(DataRow row)
            : base(row)
        { }

        public override Hashtable GetParameters()
        {
            
            Hashtable hash = base.GetParameters();
            hash.Remove("DateOfBirthString");

            return hash;
        }
    }

    public class LoginInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginInfo()
        { }
    }
}