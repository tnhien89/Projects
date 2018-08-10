using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class NewsDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int id = Utilities.GetRequestParameter("Id");
                if (id > 0)
                {
                    StartLoadNewsInfo(id);
                }
            }
        }

        private void StartLoadNewsInfo(int id)
        {
            
        }
    }
}