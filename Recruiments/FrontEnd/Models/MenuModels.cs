using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FrontEnd.Models
{
    public class Menu : BaseObject
    {
        public int Id { get; set; }
        public string Name_EN { get; set; }
        public string Name_VN { get; set; }
        public string Description_EN { get; set; }
        public string Description_VN { get; set; }
        public int ParentId { get; set; }
        public int InsertedBy { get; set; }
        public DateTime? InsertedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedDateString 
        {
            get {
                if (UpdatedDate == null || !UpdatedDate.HasValue)
                {
                    return "";
                }

                return UpdatedDate.Value.ToString("MM/dd/yyyy h:mm:ss tt");
            }
        }

        public Menu()
        { }

        public Menu(DataRow row)
            : base(row)
        { }

        public override System.Collections.Hashtable GetParameters()
        {
            Hashtable hash = base.GetParameters();
            hash.Remove("UpdatedDateString");

            return hash;
        }
    }
}