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
    public partial class ucMainRightItem : System.Web.UI.UserControl
    {
        private const string __tag = "[ucMainRightItem]";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void StartBindingItems(int otherTypeId, string title)
        {
            string tag = __tag + "[StartBindingItems]";
            LogHelpers.WriteStatus(tag, "OtherTypeId = " + otherTypeId.ToString(), "Start...");

            try
            {
                if (otherTypeId <= 0)
                {
                    return;
                }

                lbTitle.Text = title;

                var result = OtherDAL.GetAll(otherTypeId);
                if (result.Code < 0)
                {
                    LogHelpers.WriteError(tag, result.ErrorMessage);
                    return;
                }

                lvMainRightLinkItems.DataSource = result.Data;
                lvMainRightLinkItems.DataKeyNames = new string[] { "Id", "Name_VN", "Name_EN" };
                lvMainRightLinkItems.DataBind();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End.");
            }
        }

        protected void lvMainRightLinkItems_DataBound(object sender, EventArgs e)
        {

        }

        protected void lvMainRightLinkItems_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            string tag = __tag + "[lvMainRightLinkItems_ItemDataBound]";

            if (e.Item.ItemType != ListViewItemType.DataItem)
            {
                return;
            }

            LogHelpers.WriteStatus(tag, "Start");

            try
            {
                Image img = (Image)e.Item.FindControl("imgImageLink");
                img.ImageUrl = Path.Combine(Utilities.GetDirectory("ImageOtherDir"), img.ImageUrl);
                HyperLink hplName = (HyperLink)e.Item.FindControl("hplLink");
                if (Utilities.IsPhoneNumber(hplName.Text))
                {
                    hplName.NavigateUrl = "tel:" + hplName.Text;
                    hplName.Target = "";
                }
                else if (Utilities.IsPhoneNumber(hplName.NavigateUrl))
                {
                    hplName.NavigateUrl = "tel:" + hplName.NavigateUrl;
                    hplName.Target = "";
                }
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End.");
            }
        }

        
    }
}