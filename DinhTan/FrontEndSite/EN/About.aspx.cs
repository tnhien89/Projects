using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrontEndSite.EN
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int menuId = Utilities.GetMenuId("GioiThieu");
            if (menuId > 0)
            {
                this.ucAboutMain.StartDatabindingAbout(menuId);
            }
        }
    }
}