using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrontEndSite.BusinessObject;
using FrontEndSite.DataAccess;

namespace FrontEndSite.UserControls
{
    public partial class ucOnlineVisitors : System.Web.UI.UserControl
    {
        private const string __tag = "[ucOnlineVisitors]";

        protected void Page_Load(object sender, EventArgs e)
        {
            lbOnlineVisitorsValue.InnerText = Application["OnlineVisitors"].ToString();
            //---
            StartLoadSiteVisitorsInfo();
        }

        private void StartLoadSiteVisitorsInfo()
        {
            string tag = __tag + "[StartLoadSiteVisitorsInfo]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                var result = SiteVisitorsDAL.Get();
                if (result.Code < 0)
                {
                    LogHelpers.WriteError(tag, result.ErrorMessage);
                    return;
                }

                if (result.Data.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                SiteVisitorsBOL obj = new SiteVisitorsBOL(result.Data.Tables[0].Rows[0]);
                lbVisitorsInDayValue.InnerText = obj.InDay.ToString();
                lbVisitorsInMonthValue.InnerText = obj.InMonth.ToString();
                lbVisitorsInYearValue.InnerText = obj.InYear.ToString();
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
    }
}