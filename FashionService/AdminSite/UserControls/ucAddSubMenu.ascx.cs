using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite.UserControls
{
    public partial class ucAddSubMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private string StartUploadImage()
        {
            if (string.IsNullOrEmpty(FileUploadImage.FileName))
            {
                return string.Empty;
            }

            try
            {
                string folder = DateTime.Now.ToString("MMddyyyy");
                string result = string.Format("{0}/{1}", folder, FileUploadImage.FileName);

                string dir = Path.Combine(Utilities.GetDirectory("ImagesDir"), folder);
                dir = Server.MapPath("~/" + dir);

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                FileUploadImage.SaveAs(Path.Combine(dir, FileUploadImage.FileName));

                return result;
            }
            catch (Exception ex)
            {
                LogHelpers.Log.Error(ex.ToString());
                return string.Empty;
            }
        }
    }
}