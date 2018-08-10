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
    public partial class ucSubOtherInfo : System.Web.UI.UserControl
    {
        private const string __tag = "[ucSubOtherInfo]";

        private int _catId = 0;
        private int _id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            _catId = Utilities.GetRequestParameter("CatId");
            _id = Utilities.GetRequestParameter("Id");

            if (_catId <= 0)
            {
                Response.Redirect("~/Other.aspx", false);
            }

            btnSubmit.Text = "Add";

            if (!IsPostBack)
            {
                if (_id > 0)
                {
                    StartLoadSubOtherInfo(_id);
                }
            }
        }

        private void StartLoadSubOtherInfo(int id)
        {
            string tag = __tag + "[StartLoadSubOtherInfo]";
            LogHelpers.WriteStatus(tag, "Start...");

            lbError.Visible = false;

            try
            {
                var result = OtherDAL.Get(id);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;

                    return;
                }

                OtherBOL other = new OtherBOL(result.Data.Tables[0].Rows[0]);
                tbxNameVN.Text = other.Name_VN;
                tbxNameEN.Text = other.Name_EN;
                tbxLink.Text = other.Link;
                if (!string.IsNullOrEmpty(other.ImageLink))
                {
                    imgImage.ImageUrl = "~/" + Path.Combine(Utilities.GetDirectory("ImageOtherDir"), other.ImageLink);
                    imgImage.Visible = true;
                }
                else
                {
                    imgImage.Visible = false;
                }
                tbxDesVN.Text = other.Description_VN;
                tbxDesEN.Text = other.Description_EN;

                btnSubmit.Text = "Update";
                btnCancel.Visible = true;
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
                OtherBOL banner = new OtherBOL()
                {
                    Id = _id,
                    Name_VN = tbxNameVN.Text,
                    Name_EN = tbxNameEN.Text,
                    Description_VN = tbxDesVN.Text,
                    Description_EN = tbxDesEN.Text,
                    Link = tbxLink.Text,
                    ImageLink = UploadImageProcess(),
                    OtherType = _catId,
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

                StartClearData();

                Response.Redirect(Request.RawUrl, false);
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
                if (string.IsNullOrEmpty(fileUploadImage.FileName))
                {
                    return string.Empty;
                }

                string fileName = fileUploadImage.FileName;
                string dir = Server.MapPath("~/" + Utilities.GetDirectory("ImageOtherDir"));

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

                lbError.InnerText = ex.Message;
                lbError.Visible = true;

                return string.Empty;
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End.");
            }
        }

        private void StartClearData()
        {
            tbxNameVN.Text = "";
            tbxNameEN.Text = "";
            tbxLink.Text = "";
            tbxDesVN.Text = "";
            tbxDesEN.Text = "";
            imgImage.Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            StartClearData();
            btnCancel.Visible = false;
        }
    }
}