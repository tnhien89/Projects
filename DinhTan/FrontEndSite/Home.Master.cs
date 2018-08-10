using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrontEndSite
{
    public partial class Home1 : System.Web.UI.MasterPage
    {
        private const string __tag = "[Home.Master]";

        protected void Page_Load(object sender, EventArgs e)
        {
            //string tag = __tag + "[Page_Load]";

            //try
            //{
            //    string path = Request.Url.AbsolutePath;
            //    if (path.Contains("/EN/") || path == "/EN")
            //    {
            //        Session["SelectedLangue"] = "EN";
            //    }
            //    else
            //    {
            //        Session["SelectedLangue"] = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    LogHelpers.WriteException(tag, ex.ToString());    
            //}

            var menuFooter = ucFontEndMenuBar.CreateMenuBar();
            ucFrontEndFooter.CreateMenuFooter(menuFooter);
        }
    }
}