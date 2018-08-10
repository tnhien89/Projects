using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrontEndSite.BusinessObject;
using FrontEndSite.DataAccess;

namespace FrontEndSite.UserControls
{
    public partial class ucHomeNews : System.Web.UI.UserControl
    {
        private const string __tag = "[ucHomeNews]";

        protected void Page_Load(object sender, EventArgs e)
        {
            StartLoadToNews();
        }

        public void StartLoadToNews()
        {
            string tag = __tag + "[StartLoadToNews]";

            try
            {
                var result = NewsDAL.GetTopNews(10);
                if (result.Code < 0)
                {
                    LogHelpers.WriteError(tag, result.ErrorMessage);
                    return;
                }

                lvHomeNews.DataSource = result.Data;
                lvHomeNews.DataBind();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
        }

        protected void lvHomeNews_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType != ListViewItemType.DataItem)
            {
                return;
            }

            string tag = __tag + "[lvHomeNews_ItemDataBound]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                Image img = (Image)e.Item.FindControl("imgNews");

                if (string.IsNullOrEmpty(img.ImageUrl))
                {
                    img.ImageUrl = Utilities.GetNoImageUrl();
                }
                else
                {
                    img.ImageUrl = Path.Combine(Utilities.GetDirectory("ImageNewsDir"), img.ImageUrl);
                }

                NewsBOL news = new NewsBOL((e.Item.DataItem as DataRowView).Row);
                HyperLink hplName = (HyperLink)e.Item.FindControl("hplName");
                hplName.Text = Utilities.IsLangueEN() ? news.Name_EN : news.Name_VN;
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