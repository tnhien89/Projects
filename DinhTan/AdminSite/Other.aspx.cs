using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class Other : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["__EVENTARGUMENT"]) && Request["__EVENTARGUMENT"] == "DeleteItems")
                {
                    this.ucOtherItems.StartDeleteItems();
                    
                }
            }

            ucOtherItems.StartBidingOtherType();
        }
    }
}