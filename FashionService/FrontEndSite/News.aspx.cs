using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrontEndSite
{
    public partial class News : System.Web.UI.Page
    {
        private const string __navigationUrl = "NewsInfo.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            lbContentHeader.InnerText = Utilities.IsLangueEN() ? "News" : "Tin Tức";

            int id = Utilities.GetRequestParameter("Id");
            if (id > 0)
            {
                Response.Redirect(string.Format("{0}?Id={1}", __navigationUrl, id),
                    false);
            }

            int menuId = Utilities.GetMenuId("TinTuc");
            if (menuId > 0)
            {
                ucNewsList.StartBindingData(menuId, __navigationUrl);
            }
        }
    }
}