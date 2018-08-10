using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace MvcFrontEnd.BusinessObjects
{
    public class MenuBOL : BusinessObjectBase
    {
        public int Id { get; set; }
        public string Name_VN { get; set; }
        public string Name_EN { get; set; }
        public string Image { get; set; }
        public string ImageUrl {
            get {
                return Utils.GetImagesUrl(Image);
            }
        }
        public string Description_VN { get; set; }
        public string Description_EN { get; set; }
        public string RedirectUrl { get; set; }
        public string Type { get; set; }
        public int ContentId { get; set; }
        public int ParentId { get; set; }
        public DateTime InsertDate { get; set; }
        public int InsertBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        //---
        public bool IsChecked { get; set; }

        public MenuBOL()
            :base()
        { }

        public MenuBOL(DataRow row)
            : base(row)
        { }
    }
}