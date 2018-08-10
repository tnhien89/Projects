using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSite.BusinessObject;
using AdminSite.DataAccess;

namespace AdminSite
{
    public partial class ContactDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int id = Utilities.GetRequestParameter("Id");

            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    ViewState["GoBackUrl"] = Request.UrlReferrer.ToString();
                }

                if (id < 0)
                {
                    GoBackPage();
                }

                StartLoadContactDetail(id);
                return;
            }
        }

        private void StartLoadContactDetail(int id)
        {
            try
            {
                var result = ContactsDAL.Get(id);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;

                    return;
                }

                lbError.Visible = false;
                //----
                ContactBOL BOL = new ContactBOL(result.Data.Tables[0].Rows[0]);
                lbHeader.InnerText = BOL.Subject_VN;
                ltrContent.Text = BOL.Content_VN;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException("[ContactDetail][StartLoadContactDetail]", ex.ToString());
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        private void GoBackPage()
        {
            string url = "~/Default.aspx";
            if (ViewState["GoBackUrl"] != null)
            {
                url = ViewState["GoBackUrl"].ToString();
            }

            Response.Redirect(url, false);
        }
    }
}