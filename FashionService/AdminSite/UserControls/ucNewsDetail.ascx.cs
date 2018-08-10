using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSite.BusinessObject;
using AdminSite.DataAccess;

namespace AdminSite.UserControls
{
    public partial class ucNewsDetail : System.Web.UI.UserControl
    {
        private string _tag = "[ucNewsDetail]";
        private int _menuId;

        protected void Page_Load(object sender, EventArgs e)
        {
            _menuId = Utilities.GetRequestParameter("MenuId");
            int id = Utilities.GetRequestParameter("Id");

            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    ViewState["GoBackUrl"] = Request.UrlReferrer.ToString();
                }

                if (id > 0)
                {
                    StartLoadNewsInfo(id);
                }

                StartLoadVacancies();
            }
        }

        private void StartLoadVacancies()
        {
            string tag = _tag + "[StartLoadVacancies]";
            var result = VacanciesDAL.GetAll();
            if (result.Code < 0)
            {
                LogHelpers.WriteError(tag, result.ErrorMessage);
                return;
            }

            ddlVacancy.DataSource = result.Data;
            ddlVacancy.DataValueField = "Id";
            ddlVacancy.DataTextField = "Name_VN";
            //---
            ddlVacancyEN.DataSource = result.Data;
            ddlVacancyEN.DataValueField = "Id";
            ddlVacancyEN.DataTextField = "Name_EN";
            //---
            ddlVacancy.DataBind();
            ddlVacancyEN.DataBind();
        }

        private void StartLoadNewsInfo(int id)
        {
            string tag = _tag + "[StartLoadNewsInfo]";

            var result = NewsDAL.Get(id);
            if (result.Code < 0)
            {
                LogHelpers.WriteError(tag, result.ErrorMessage);
                lbError.InnerText = result.ErrorMessage;
                lbError.Visible = true;

                return;
            }

            lbError.Visible = false;
            if (result.Data.Tables[0].Rows.Count == 0)
            {
                return;
            }

            NewsBOL BOL = new NewsBOL(result.Data.Tables[0].Rows[0]);
            StartShowData(BOL);
        }

        private void StartShowData(NewsBOL BOL)
        {
            string tag = _tag + "[StartShowData]";
            try
            {
                tbxNameVN.Text = BOL.Name_VN;
                tbxDesVN.Text = BOL.Description_VN;
                txaContentVN.Text = Utilities.SetFullLinkImage(BOL.Content_VN,
                    Utilities.GetDirectory("ImagesDir"));
                //---
                tbxNameEN.Text = BOL.Name_EN;
                tbxDesEN.Text = BOL.Description_EN;

                if (!string.IsNullOrEmpty(BOL.ImageLink))
                {
                    imgImage.ImageUrl = "~/" + Path.Combine(Utilities.GetDirectory("ImagesDir"), BOL.ImageLink);
                    imgImage.Visible = true;
                }
                else
                {
                    imgImage.Visible = false;
                }

                txaContentEN.Text = Utilities.SetFullLinkImage(BOL.Content_EN,
                    Utilities.GetDirectory("ImagesDir"));

                ddlVacancy.Items.FindByValue(BOL.VacancyId).Selected = true;
                ddlVacancyEN.Items.FindByValue(BOL.VacancyId).Selected = true;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError(tag, ex.ToString());
            }
        }

        private void GoBackPage()
        {
            string url = "~/Default.aspx";
            if (ViewState["GoBackUrl"] != null)
            {
                url = ViewState["GoBackUrl"].ToString();
            }

            Response.Redirect(url.ToString(), false);
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                NewsBOL BOL = CreateNewsObject();
                var result = NewsDAL.InsertOrUpdate(BOL);

                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;

                    return;
                }

                lbError.Visible = false;
                GoBackPage();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError("[ucNewsItems][btnSubmit_Click]", ex.ToString());
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            GoBackPage();
        }

        private NewsBOL CreateNewsObject()
        {
            NewsBOL result = new NewsBOL() { 
                Name_VN = tbxNameVN.Text,
                Description_VN = tbxDesVN.Text,
                Content_VN = txaContentVN.Text,
                //---
                Name_EN = tbxNameEN.Text,
                Description_EN = tbxDesEN.Text,
                Content_EN = txaContentEN.Text,
                //---
                InsertDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            result.ImageLink = StartUploadImage();

            if (_menuId > 0)
            {
                result.MenuId = _menuId;
                result.ParentId = _menuId;
            }

            int newsId = Utilities.GetRequestParameter("Id");
            if (newsId > 0)
            {
                result.Id = newsId;
            }

            return result;
        }

        private string StartUploadImage()
        {
            if (string.IsNullOrEmpty(FileUploadImage.FileName))
            {
                return string.Empty;
            }

            string tag = _tag + "[StartUploadImage]";

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
                LogHelpers.WriteException(tag, ex.ToString());
                return string.Empty;
            }
        }

        #region public function
        public void ShowCavancy(bool value)
        {
            this.groupVacancyVN.Visible = value;
            this.groupVacancyEN.Visible = value;

            this.rfvVacancy.Enabled = value;
        }
        #endregion
    }
}