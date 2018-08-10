using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrontEndSite
{
    public partial class MainFrontEnd : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //string path = Request.Url.AbsolutePath;
            //if (path.Contains("/EN/") || path == "/EN")
            //{
            //    Session["SelectedLangue"] = "EN";
            //}
            //else
            //{
            //    Session["SelectedLangue"] = null;
            //}

            var menuFooter = ucFontEndMenuBar.CreateMenuBar();
            ucFrontEndFooter.CreateMenuFooter(menuFooter);
        }
    }
}