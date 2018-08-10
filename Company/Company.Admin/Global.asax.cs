using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Company.Admin
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            HttpContext context = HttpContext.Current;

            if (context.Session != null && context.Session["LoggedInfo"] == null && !app.Request.AppRelativeCurrentExecutionFilePath.Contains("/Login") && !app.Request.AppRelativeCurrentExecutionFilePath.Contains("__browserLink"))
            {
                Response.RedirectToRoute("Login");
            }
        }
    }
}
