using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class OtherDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["__EVENTARGUMENT"]) && Request["__EVENTARGUMENT"] == "DeleteItems")
                {
                    this.ucSubOtherItems.StartDeleteItems();
                    
                }
            }
            int catId = Utilities.GetRequestParameter("CatId");
            if (catId > 0)
            {
                this.ucSubOtherItems.StartBidingOther(catId);
            }
        }
    }
}