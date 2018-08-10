using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSite.BusinessObject;
using AdminSite.DataAccess;

namespace AdminSite
{
    public partial class ProjectDetail : System.Web.UI.Page
    {
        private string _tag = "[ProjectDetail]";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    ViewState["GoBackUrl"] = Request.UrlReferrer.ToString();
                }
                //---
                //StartBindingCategory();
                //----
                int id = Utilities.GetRequestParameter("Id");
                if (id > 0)
                {
                    StartLoadProjectInfo(id);
                }
            }
        }

        private void StartBindingCategory()
        {
            string tag = _tag + "[StartBindingCategory]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                var result = MenuDAL.GetAllSubMenuProjects();
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;
                    LogHelpers.WriteError(tag, result.ErrorMessage);

                    return;
                }


            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //--
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End.");
            }
        }

        private void StartLoadProjectInfo(int id)
        {
            if (id <= 0)
            {
                return;
            }

            var result = ProjectsDAL.Get(id);
            if (result.Code < 0)
            {
                lbError.InnerText = result.ErrorMessage;
                lbError.Visible = true;

                return;
            }

            lbError.Visible = false;
            //---
            ProjectBOL BOL = new ProjectBOL(result.Data.Tables[0].Rows[0]);
            if (BOL == null)
            {
                return;
            }

            StartLoadProjectImages(BOL.ImageLink);

            tbxNameVN.Text = BOL.Name_VN;
            tbxDesVN.Text = BOL.Description_VN;
            tbxAddress_VN.Text = BOL.Address_VN;
            txaContentVN.Text = Utilities.SetFullLinkImage(BOL.Content_VN,
                Utilities.GetDirectory("ImageProjectsDir"));
            //chkChooseCategory.Checked = BOL.IsRedirectUrl == "Yes" ? true : false;
            //tbxRedirectLink.Text = BOL.RedirectUrl;
            //---
            tbxNameEN.Text = BOL.Name_EN;
            tbxDesEN.Text = BOL.Description_EN;
            tbxAddress_EN.Text = BOL.Address_EN;
            txaContentEN.Text = Utilities.SetFullLinkImage(BOL.Content_EN,
                Utilities.GetDirectory("ImageProjectsDir"));
            //---
            if (BOL.StartDate != null &&
                BOL.StartDate != DateTime.MinValue)
            {
                tbxStartDate.Text = BOL.StartDate.ToString("MM/dd/yyyy");
            }

            if (BOL.EndDate != null &&
                BOL.EndDate != DateTime.MinValue)
            {
                tbxEndDate.Text = BOL.EndDate.ToString("MM/dd/yyyy");
            }
        }

        private void StartLoadProjectImages(string imageLinks)
        {
            string tag = _tag + "[StartLoadProjectImages]";
            LogHelpers.WriteStatus(tag, "ImageLinks: " + imageLinks, "Start...");
            //---
            if (string.IsNullOrEmpty(imageLinks))
            {
                return;
            }

            try
            {
                hfListImage.Value = imageLinks;
                //---
                string[] images = imageLinks.Split('|');
                if (images != null && images.Length > 0)
                {
                    lvProjectImages.DataSource = images.ToList();
                    lvProjectImages.DataBind();
                }
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "ImageLinks: " + imageLinks, "End.");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ProjectBOL BOL = CreateNewProject();
            //---
            if (BOL == null)
            {
                return;
            }

            var result = ProjectsDAL.InsertOrUpdate(BOL);
            if (result.Code < 0)
            {
                lbError.InnerText = result.ErrorMessage;
                lbError.Visible = true;

                return;
            }

            lbError.Visible = false;
            //----
            GoBackPage();
        }

        private void GoBackPage()
        {
            string url = "~/Default.aspx";
            if (ViewState["GoBackUrl"] != null)
            {
                url = ViewState["GoBackUrl"].ToString();
            }

            Response.Redirect(url, false);
        }

        private ProjectBOL CreateNewProject()
        {
            string tag = _tag + "[CreateNewProject]";

            try
            {
                DateTime startDate = DateTime.MinValue;
                if (!string.IsNullOrEmpty(tbxStartDate.Text) &&
                    !DateTime.TryParseExact(tbxStartDate.Text, "MM/dd/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out startDate))
                {
                    startDate = DateTime.MinValue;
                }

                DateTime endDate = DateTime.MinValue;
                if (!string.IsNullOrEmpty(tbxEndDate.Text) &&
                    !DateTime.TryParseExact(tbxEndDate.Text, "MM/dd/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out endDate))
                {
                    endDate = DateTime.MinValue;
                }

                ProjectBOL BOL = new ProjectBOL() { 
                    Name_VN = tbxNameVN.Text,
                    Description_VN = tbxDesVN.Text,
                    Content_VN = txaContentVN.Text,
                    Address_VN = tbxAddress_VN.Text,
                    //---
                    Name_EN = tbxNameEN.Text,
                    Description_EN = tbxDesEN.Text,
                    Address_EN = tbxAddress_EN.Text,
                    Content_EN = txaContentEN.Text,
                    StartDate = startDate,
                    EndDate = endDate,
                    //IsRedirectUrl = chkChooseCategory.Checked ? "Yes" : "No",
                    //RedirectUrl = tbxRedirectLink.Text.Trim(),
                    //--
                    ImageLink = hfListImage.Value,
                    //---
                    InsertDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };
                //---
                int menuId = Utilities.GetRequestParameter("MenuId");
                if (menuId > 0)
                {
                    BOL.MenuId = menuId;
                }

                int id = Utilities.GetRequestParameter("Id");
                if (id > 0)
                {
                    BOL.Id = id;
                }

                return BOL;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError(string.Format("{0} Exception: {1}", tag, ex.ToString()));
                return null;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            GoBackPage();
        }

        protected void lvProjectImages_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            string tag = _tag + "[lvProjectImages_ItemDataBound]";
            //---
            if (e.Item.ItemType != ListViewItemType.DataItem)
            {
                return;
            }

            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                string imageLink = e.Item.DataItem.ToString();
                HiddenField hfImage = (HiddenField)e.Item.FindControl("hfImageLink");
                Image img = (Image)e.Item.FindControl("itemImage");
                //---
                hfImage.Value = imageLink;
                img.ImageUrl = "~/" + Path.Combine(Utilities.GetDirectory("ImageProjectsDir"), imageLink);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End...");
            }
        }
    }
}