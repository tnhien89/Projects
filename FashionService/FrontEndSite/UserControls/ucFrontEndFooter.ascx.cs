using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrontEndSite.BusinessObject;

namespace FrontEndSite.UserControls
{
    public partial class ucFrontEndFooter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CreateMenuFooter(List<MenuBOL> list)
        {
            ucMenuFooter.CreateMenuFooter(list);
        }
    }
}