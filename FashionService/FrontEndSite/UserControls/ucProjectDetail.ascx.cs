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
    public partial class ucProjectDetail : System.Web.UI.UserControl
    {
        private const string __tag = "[ucProjectDetail]";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void StartShowProjectDetail(int projectId)
        {
            string tag = __tag + "[StartShowProjectDetail]";
            LogHelpers.WriteStatus(tag, "PeojectId = " + projectId.ToString(), "Start.");

            if (projectId <= 0)
            {
                return;
            }

            try
            {
                var result = ProjectsDAL.Get(projectId);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;
                    //--
                    LogHelpers.WriteError(tag, result.ErrorMessage);

                    return;
                }

                lbError.Visible = false; 
                //--
                ProjectBOL project = new ProjectBOL(result.Data.Tables[0].Rows[0]);
                lbHeader.InnerText = Utilities.IsLangueEN() ? project.Name_EN : project.Name_VN;
                lbUpdatedDate.InnerText = project.UpdatedDate.ToString("MM/dd/yyyy HH:mm");
                //---
                ltrContent.Text = Utilities.SetFullLinkImage(
                    Utilities.IsLangueEN() ? project.Content_EN : project.Content_VN,
                    Utilities.GetDirectory("ImageProjectsDir"));
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //---
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }

            LogHelpers.WriteStatus(tag, "PeojectId = " + projectId.ToString(), "End.");
        }
    }
}