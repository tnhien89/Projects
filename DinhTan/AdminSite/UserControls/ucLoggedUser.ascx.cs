using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite.UserControls
{
    public partial class ucLoggedUser : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lbFullName.InnerText = Utilities.GetLoggedFullName();
            hplUserProfile.NavigateUrl = string.Format("{0}?Id={1}",
                "~/Users.aspx", Utilities.GetLoggedId());
            hplLogout.NavigateUrl = string.Format("{0}?Action=LogOut",
                "~/Default.aspx");
        }
    }
}