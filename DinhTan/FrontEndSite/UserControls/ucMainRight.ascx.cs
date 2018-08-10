using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrontEndSite.DataAccess;

namespace FrontEndSite.UserControls
{
    public partial class ucMainRight : System.Web.UI.UserControl
    {
        private const string __tag = "[ucMainRight]";

        protected void Page_Load(object sender, EventArgs e)
        {
            StartBindingOtherType();
        }

        private void StartBindingOtherType()
        {
            string tag = __tag + "[StartBindingOtherType]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                var result = OtherTypeDAL.GetAll(false);
                if (result.Code < 0)
                {
                    LogHelpers.WriteError(tag, result.ErrorMessage);
                    return;
                }

                lvMainRight.DataSource = result.Data;
                lvMainRight.DataKeyNames = new string[] { "Id", "Name_VN", "Name_EN" };
                lvMainRight.DataBind();
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

        protected void lvMainRight_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            string tag = __tag + "[lvMainRight_ItemDataBound]";

            if (e.Item.ItemType != ListViewItemType.DataItem)
            {
                return;
            }

            LogHelpers.WriteStatus(tag, "Start...");
            try
            {
                ucMainRightItem mainRightItem = (ucMainRightItem)e.Item.FindControl("ucMainRightItem");
                string id = lvMainRight.DataKeys[e.Item.DataItemIndex]["Id"].ToString();
                string title = "";
                if (Utilities.IsLangueEN())
                {
                    title = lvMainRight.DataKeys[e.Item.DataItemIndex]["Name_EN"].ToString();
                }
                else
                { 
                    title = lvMainRight.DataKeys[e.Item.DataItemIndex]["Name_VN"].ToString();
                }

                mainRightItem.StartBindingItems(int.Parse(id), title);
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