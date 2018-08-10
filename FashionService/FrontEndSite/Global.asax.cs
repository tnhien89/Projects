using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using FrontEndSite.DataAccess;

namespace FrontEndSite
{
    public class Global : System.Web.HttpApplication
    {
        private const string __tag = "[Global]";

        protected void Application_Start(object sender, EventArgs e)
        {
            Application["OnlineVisitors"] = 0;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["OnlineVisitors"] = (int)Application["OnlineVisitors"] + 1;
            Application.UnLock();
            //---
            StartUpdateSiteVisitors();
        }

        private void StartUpdateSiteVisitors()
        {
            string tag = __tag + "[StartUpdateSiteVisitors]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                var result = SiteVisitorsDAL.UpdateSiteVisitors();
                if (result.Code < 0)
                {
                    LogHelpers.WriteError(tag, result.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End.");
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Application.Lock();
            if (Application["OnlineVisitors"] != null && 
                (int)Application["OnlineVisitors"] != 0)
            {
                Application["OnlineVisitors"] = (int)Application["OnlineVisitors"] - 1;
            }
            Application.UnLock();
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}