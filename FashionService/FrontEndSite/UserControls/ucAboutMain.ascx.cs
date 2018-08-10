using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrontEndSite.BusinessObject;
using FrontEndSite.DataAccess;

namespace FrontEndSite.UserControls
{
    public partial class ucAboutMain : System.Web.UI.UserControl
    {
        private const string __tag = "[ucAboutMain]";

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void StartDatabindingAbout(int menuId)
        {
            string tag = __tag + "[StartDatabindingAbout]";

            if (menuId <= 0)
            {
                return;
            }

            try
            {
                var result = NewsDAL.GetAll(menuId);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;
                    //--
                    LogHelpers.WriteError(tag, result.ErrorMessage);

                    return;
                }

                lvAbout.DataSource = result.Data;
                lvAbout.DataBind();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //---
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        protected void lvAbout_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            string tag = __tag + "[lvAbout_ItemDataBound]";

            if (e.Item.ItemType != ListViewItemType.DataItem)
            {
                return;
            }

            try
            {
                DataRowView row = (DataRowView)e.Item.DataItem;
                NewsBOL news = new NewsBOL(row.Row);
                //----
                Label lbName = (Label)e.Item.FindControl("lbName");
                lbName.Text = Utilities.IsLangueEN() ? news.Name_EN : news.Name_VN;

                Literal ltrContent = (Literal)e.Item.FindControl("ltrContent");
                ltrContent.Text = Utilities.IsLangueEN() ? 
                    Utilities.SetFullLinkImage(news.Content_EN, Utilities.GetDirectory("ImageNewsDir")) :
                    Utilities.SetFullLinkImage(news.Content_VN, Utilities.GetDirectory("ImageNewsDir"));
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
        }
    }
}