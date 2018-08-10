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
                    this.ucSubOtherItems.StartDeleteItems();

                }
            }

            bool isGroup = false;
            int id = Utilities.GetRequestParameter("GroupId");
            if (id > 0)
            {
                isGroup = true;
            }
            else
            {
                id = Utilities.GetRequestParameter("Id");
            }

            if (id <= 0)
            {
                ucSubOtherItems.Visible = false;
                ucOtherInfo.Visible = false;
                ucOtherItems.Visible = true;
                ucOtherItems.StartBidingOtherType(id);
            }
            else
            {
                ucOtherItems.Visible = false;
                ucSubOtherItems.Visible = true;
                ucOtherInfo.Visible = true;
                ucSubOtherItems.StartBidingOther(id);
            }
        }
    }
}