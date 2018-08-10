using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AdminSite.BusinessObject
{
    public class GalleryBOL : BOL
    {
        public int Id { get; set; }
        public string Name_VN { get; set; }
        public string Name_EN { get; set; }
        public string Image { get; set; }
        public string Description_VN { get; set; }
        public string Description_EN { get; set; }
        public bool IsGroup { get; set; }
        public int ParentId { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public GalleryBOL()
            :base()
        {}

        public GalleryBOL(DataRow row)
            : base(row)
        { }

        public override System.Collections.Hashtable GetParameters()
        {
            Hashtable hash = base.GetParameters();
            hash.Remove("UpdatedDate");

            return hash;
        }
    }
}