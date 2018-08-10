using AdminSite.BusinessObject;
using AdminSite.DataAccess;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class Main : System.Web.UI.MasterPage
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utilities.GetLoggedId() <= 0)
            {
                Response.Redirect("Default.aspx", false);
                return;
            }

            GetLogo();
        }

        private void GetLogo()
        {
            ResultBOL<DataSet> result = AboutsDAL.Get();
            if (result.Code < 0)
            {
                _log.Error(result.ErrorMessage);
            }
            else if (result.Data != null && result.Data.Tables.Count > 0 && result.Data.Tables[0].Rows.Count > 0)
            {
                AboutBOL about = new AboutBOL(result.Data.Tables[0].Rows[0]);
                if (!string.IsNullOrEmpty(about.ImageLink))
                {
                    this.imgLogo.Src = "~/" + Path.Combine(Utilities.GetDirectory("ImagesDir"), about.ImageLink);
                }
            }
        }
    }
}