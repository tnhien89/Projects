using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSite.BusinessObject;
using AdminSite.DataAccess;

namespace AdminSite.UserControls
{
    public partial class ucBanner : System.Web.UI.UserControl
    {
        private const string __tag = "[ucBanner]";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void HideDropDownListType()
        {
            formType.Visible = false;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lbError.Visible = false;
            InsertOrUpdateBannerProcess();
        }

        private void InsertOrUpdateBannerProcess()
        {
            string tag = __tag + "[InsertOrUpdateBannerProcess]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                OtherBOL banner = new OtherBOL() { 
                    Name_VN = tbxNameVN.Text,
                    Name_EN = tbxNameEN.Text,
                    Description_VN = tbxDesVN.Text,
                    Description_EN = tbxDesEN.Text,
                    Link = tbxLink.Text,
                    ImageLink = UploadImageProcess(),
                    InsertDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };

                var result = OtherDAL.InsertOrUpdate(banner);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;

                    return;
                }
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());

                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End.");
            }
        }

        private string UploadImageProcess()
        {
            string tag = __tag + "[UploadImageProcess]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                string fileName = fileUploadImage.FileName;
                string dir = Server.MapPath("~/" + Utilities.GetDirectory("ImageBannerDir"));

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                fileUploadImage.SaveAs(Path.Combine(dir, fileName));

                return fileName;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());

                return string.Empty;
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End");
            }
        }
    }
}