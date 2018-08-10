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
    public partial class ucNewsList : System.Web.UI.UserControl
    {
        private const string __tag = "[ucNewsList]";
        //--
        private string _navigationUrl;
        private int _menuId;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void StartBindingData(object dataSource, int menuId, string navigationUrl)
        {
            _navigationUrl = navigationUrl;
            _menuId = menuId;

            if (dataSource == null)
            {
                return;
            }

            lvNews.DataSource = dataSource;
            lvNews.DataBind();
        }

        public void StartBindingData(int menuId, string navigationUrl)
        {
            string tag = __tag + "[StartBindingData]";
            //--
            LogHelpers.WriteStatus(tag, "MenuId = " + menuId.ToString(), "Start.");
            _navigationUrl = navigationUrl;

            try
            {
                var result = NewsDAL.GetAll(menuId);
                if (result.Code < 0)
                {
                    return;
                }
                //---

                lvNews.DataSource = result.Data;
                lvNews.DataKeyNames = new string[] { "Id" };
                lvNews.DataBind();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }

            LogHelpers.WriteStatus(tag, "MenuId = " + menuId.ToString(), "End.");
        }

        protected void lvNews_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            string tag = __tag + "[lvNews_ItemDataBound]";

            if (e.Item.ItemType != ListViewItemType.DataItem)
            {
                return;
            }

            try
            {
                Image img = (Image)e.Item.FindControl("imgImageLink");

                if (string.IsNullOrEmpty(img.ImageUrl))
                {
                    img.ImageUrl = Utilities.GetApplicationSettingsValue("NoImageUrl");
                }
                else
                {
                    img.ImageUrl = Path.Combine(Utilities.GetDirectory("ImageNewsDir"),
                        img.ImageUrl);
                }

                DataRowView rowView = (DataRowView)e.Item.DataItem;
                NewsBOL news = new NewsBOL(rowView.Row);

                HyperLink hplDetail = (HyperLink)e.Item.FindControl("hplName");
                hplDetail.Text = Utilities.IsLangueEN() ? news.Name_EN : news.Name_VN;
                hplDetail.NavigateUrl = string.Format("~/{0}?Id={1}", 
                    _navigationUrl,
                    hplDetail.NavigateUrl);

                Label lbDes = (Label)e.Item.FindControl("lbDes");
                if (!string.IsNullOrEmpty(news.Description_EN) || !string.IsNullOrEmpty(news.Description_VN))
                {
                    lbDes.Text = Utilities.IsLangueEN() ? news.Description_EN : news.Description_VN;
                }
                else
                { 
                    lbDes.Text = Utilities.IsLangueEN() ? news.Content_EN : news.Content_VN;
                }
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
        }
    }
}