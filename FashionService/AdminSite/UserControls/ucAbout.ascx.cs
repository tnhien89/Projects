using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSite.BusinessObject;
using AdminSite.DataAccess;
using System.IO;
using NLog;

namespace AdminSite.UserControls
{
    public partial class ucAbout : System.Web.UI.UserControl
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void StartLoadAboutDetail()
        {
            string tag = "[ucAbout][StartLoadAboutDetail]";

            var result = AboutsDAL.Get();
            if (result.Code < 0)
            {
                lbError.InnerText = result.ErrorMessage;
                lbError.Visible = true;
                return;
            }

            lbError.Visible = false;
            try
            {
                AboutBOL about = new AboutBOL(result.Data.Tables[0].Rows[0]);
                hfId.Value = about.Id.ToString();
                tbxNameVN.Text = about.Name_VN;
                tbxNameEN.Text = about.Name_EN;
                tbxAddress.Text = about.Address_VN;
                tbxAddressEN.Text = about.Address_EN;
                tbxFax.Text = about.Fax;
                tbxPhone.Text = about.Phone;
                tbxEmail.Text = about.Email;
                tbxWebSite.Text = about.WebSite;
                tbxGoogleCode.Text = about.GoogleMaps;
                if (!string.IsNullOrEmpty(about.ImageLink))
                    imgImage.ImageUrl = "~/" + Path.Combine(Utilities.GetDirectory("ImagesDir"), about.ImageLink);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //---
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AboutBOL obj = CreateAboutObject();
            if (!string.IsNullOrEmpty(obj.ImageLink))
                imgImage.ImageUrl = "~/" + Path.Combine(Utilities.GetDirectory("ImagesDir"), obj.ImageLink);
            //--
            var result = AboutsDAL.InsertOrUpdate(obj);
            if (result.Code < 0)
            {
                lbError.InnerText = result.ErrorMessage;
                lbError.Visible = true;

                return;
            }

            lbError.Visible = false;
        }

        private AboutBOL CreateAboutObject()
        {
            string tag = "[ucAbout][CreateAboutObject]";

            try
            {
                AboutBOL about = new AboutBOL() { 
                    Id = string.IsNullOrEmpty(hfId.Value) ? 0 : int.Parse(hfId.Value),
                    Name_VN = tbxNameVN.Text,
                    Name_EN = tbxNameEN.Text,
                    Address_VN = tbxAddress.Text,
                    Address_EN = tbxAddressEN.Text,
                    Phone = tbxPhone.Text,
                    Fax = tbxFax.Text,
                    Email = tbxEmail.Text,
                    WebSite = tbxWebSite.Text,
                    GoogleMaps = tbxGoogleCode.Text
                };

                about.ImageLink = StartUploadImage();

                //--
                return about;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                lbError.InnerText = ex.Message;
                lbError.Visible = true;

                return null;
            }
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
                _log.Error(ex.ToString());
                return string.Empty;
            }
        }
    }
}