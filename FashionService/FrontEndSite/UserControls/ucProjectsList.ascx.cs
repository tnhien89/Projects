using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FrontEndSite.BusinessObject;
using FrontEndSite.DataAccess;

namespace FrontEndSite.UserControls
{
    public partial class ucProjectsList : System.Web.UI.UserControl
    {
        private const string __tag = "[ucProjectsList]";
        //----
        private string _navigationUrl;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void StartBindingProjects(int menuId, string navigationUrl, bool isLoadAll)
        {
            string tag = __tag + "[StartBindingProjects]";
            //---
            LogHelpers.WriteStatus(tag, 
                "MenuId = " + menuId.ToString(),
                "Start.");

            if (menuId <= 0)
            {
                return;
            }

            StartLoadHeder(menuId);
            //---

            _navigationUrl = navigationUrl;

            try
            {
                var result = ProjectsDAL.GetTopProjects(int.Parse(Utilities.GetApplicationSettingsValue("Project-Items")),
                    isLoadAll ? 0 : menuId);

                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;
                    //---
                    hfScanTime.Value = result.Code.ToString();
                    hfFirstScanTime.Value = hfScanTime.Value;
                    //---
                    LogHelpers.WriteError(tag, result.ErrorMessage);

                    return;
                }

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    hfScanTime.Value = "0";
                    hfFirstScanTime.Value = hfScanTime.Value;
                    return;
                }

                string scanTime = Utilities.GetDataUpdatedDate(result.Data.Tables[0].Rows[result.Data.Tables[0].Rows.Count - 1]);
                hfMenuId.Value = isLoadAll ? "0" : menuId.ToString();
                hfScanTime.Value = scanTime;
                hfFirstScanTime.Value = hfScanTime.Value;
                
                lvProjects.DataSource = result.Data;
                lvProjects.DataKeyNames = new string[] { "Id" };
                lvProjects.DataBind();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //--
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }

            LogHelpers.WriteStatus(tag, "End.");
        }

        private void StartLoadHeder(int menuId)
        {
            string tag = __tag + "[StartLoadHeder]";

            try
            {
                var result = MenuDAL.GetMenuName(menuId);
                if (result.Code < 0)
                {
                    LogHelpers.WriteError(tag, result.ErrorMessage);
                    return;
                }

                //lbHeader.InnerText = result.Data.ToString();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
        }

        protected void lvProjects_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            string tag = __tag + "[lvProjects_ItemDataBound]";
            //---
            if (e.Item.ItemType != ListViewItemType.DataItem)
            {
                return;
            }

            try
            {
                DataRowView rowView = (DataRowView)e.Item.DataItem;
                ProjectBOL project = new ProjectBOL(rowView.Row);

                HyperLink hplName = (HyperLink)e.Item.FindControl("hplName");
                string id = hplName.NavigateUrl;

                hplName.NavigateUrl = string.Format("~/{0}?Id={1}", _navigationUrl, hplName.NavigateUrl);
                //----
                Image ctrlImage = (Image)e.Item.FindControl("imageLink");
                ctrlImage.ImageUrl = Path.Combine(Utilities.GetDirectory("ImageProjectsDir"), ctrlImage.ImageUrl.Split('|')[0]);

                //if (!string.IsNullOrEmpty(project.IsRedirectUrl) && 
                //    project.IsRedirectUrl.Contains("Yes"))
                //{
                //    ctrlImage.Attributes.Add("onclick",
                //    string.Format("javascript:window.location.href='{0}'",
                //        project.RedirectUrl));
                //}
                //else
                //{
                //    ctrlImage.Attributes.Add("onclick",
                //        string.Format("javascript:window.location.href='{0}?Id={1}'",
                //            _navigationUrl,
                //            id));
                //}

                ctrlImage.Attributes.Add("onclick",
                        string.Format("javascript:window.location.href='{0}?Id={1}'",
                            _navigationUrl,
                            id));
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
        }
    }
}