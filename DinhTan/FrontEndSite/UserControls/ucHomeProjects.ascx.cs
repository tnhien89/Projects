using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrontEndSite.DataAccess;

namespace FrontEndSite.UserControls
{
    public partial class ucHomeProjects : System.Web.UI.UserControl
    {
        private const string __tag = "[ucHomeProjects]";
        private const string __navigationUrl = "ProjectsInfo.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            StartLoadTopNew();
        }

        public void StartLoadTopNew()
        {
            string tag = __tag + "[StartLoadTopNew]";
            LogHelpers.WriteStatus(tag, "Start.");
            try
            {
                var result = ProjectsDAL.GetTopProjects(
                    int.Parse(Utilities.GetApplicationSettingsValue("HomeProject-Items")),
                    0);

                if (result.Code < 0)
                {
                    LogHelpers.WriteError(tag, result.ErrorMessage);
                    hfScanTime.Value = "-1";
                    hfScanTimeFirstLoad.Value = hfScanTime.Value;

                    return;
                }

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    hfScanTime.Value = "0";
                    hfScanTimeFirstLoad.Value = hfScanTime.Value;
                    return;
                }

                int rowCount = result.Data.Tables[0].Rows.Count;

                hfScanTime.Value = Utilities.GetDataUpdatedDate(result.Data.Tables[0].Rows[rowCount - 1]);
                hfScanTimeFirstLoad.Value = hfScanTime.Value;

                lvHomeProjects.DataSource = result.Data;
                lvHomeProjects.DataBind();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }

            LogHelpers.WriteStatus(tag, "End.");
        }

        protected void lvHomeProjects_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            string tag = __tag + "[lvHomeProjects_ItemDataBound]";
            if (e.Item.ItemType != ListViewItemType.DataItem)
            {
                return;
            }

            try
            {
                HyperLink hplName = (HyperLink)e.Item.FindControl("hplName");
                string id = hplName.NavigateUrl;
                //---
                hplName.NavigateUrl = string.Format("~/{0}?Id={1}", __navigationUrl, hplName.NavigateUrl);
                //----
                Image ctrlImage = (Image)e.Item.FindControl("imageLink");
                ctrlImage.ImageUrl = Path.Combine(Utilities.GetDirectory("ImageProjectsDir"), ctrlImage.ImageUrl.Split('|')[0]);
                ctrlImage.Attributes.Add("onclick",
                    string.Format("javascript:window.location.href='{0}?Id={1}'",
                        __navigationUrl,
                        id));
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
        }
    }
}