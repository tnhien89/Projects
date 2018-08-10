using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Models
{
    public class News : BaseObject
    {
        public int Id { get; set; }
        public string Title_EN { get; set; }
        public string Title_VN { get; set; }

        [AllowHtml]
        public string Content_EN { get; set; }

        [AllowHtml]
        public string Content_VN { get; set; }
        public string Description_EN { get; set; }
        public string Description_VN { get; set; }
        public int? MenuId { get; set; }
        public string MenuName_EN { get; set; }
        public string MenuName_VN { get; set; }
        public int? CategoryId { get; set; }
        public int? InsertedBy { get; set; }
        public DateTime? InsertedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedDateString { 
            get {
                if (UpdatedDate == null || !UpdatedDate.HasValue)
                {
                    return "";
                }

                return UpdatedDate.Value.ToString("MM/dd/yyyy h:mm:ss tt");
            }
        }

        public News()
        { }

        public News(DataRow row)
            :base(row)
        { }

        public override System.Collections.Hashtable GetParameters()
        {
            Hashtable hash = base.GetParameters();
            hash.Remove("MenuName_EN");
            hash.Remove("MenuName_VN");
            hash.Remove("UpdatedDateString");

            return hash;
        }
    }
}