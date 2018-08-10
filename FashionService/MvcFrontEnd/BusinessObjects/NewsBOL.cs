using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcFrontEnd.BusinessObjects
{
    public class NewsBOL : BusinessObjectBase
    {
        public int Id { get; set; }
        public string Name_VN { get; set; }
        public string Name_EN { get; set; }
        public string ImageLink { get; set; }
        public string ImageUrl { 
            get {
                return Utils.GetImagesUrl(ImageLink);
            } 
        }
        public string Description_VN { get; set; }
        public string Description_EN { get; set; }
        public string VacancyId { get; set; }

        private string _content_VN;
        public string Content_VN 
        {
            get {
                return Utils.SetFullLinkImage(_content_VN);
            }
            set {
                _content_VN = value;
            }
        }

        private string _content_EN;
        public string Content_EN 
        {
            get {
                return Utils.SetFullLinkImage(_content_EN);
            }

            set {
                _content_EN = value;
            }
        }
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
    }
}
