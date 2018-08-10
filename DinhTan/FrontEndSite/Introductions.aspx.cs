using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrontEndSite
{
    public partial class Introductions : System.Web.UI.Page
    {
        private const string __navigationUrl = "Introductions.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {

            int id = Utilities.GetRequestParameter("Id");
            int menuId = Utilities.GetMenuId("GioiThieu");
            if (menuId > 0)
            {
                ucNewsMain.StartBindingDataNews(menuId, id, __navigationUrl, "Giới Thiệu");
            }
        }
    }
}