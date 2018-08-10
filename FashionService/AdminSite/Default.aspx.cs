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
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request["Action"] != null && Request["Action"].ToString() == "LogOut")
            {
                Session.Clear();

                string url = Request.RawUrl.Split('?')[0];
                Response.Redirect(url, false);

                return;
            }

            if (Utilities.GetLoggedId() > 0)
            {
                GoToHome();
                return;
            }

            if (IsPostBack)
            {
                StartCheckLogin();
            }
        }

        private void StartCheckLogin()
        {
            try
            {
                lbError.InnerText = "";
                lbError.Visible = false;
                string username = tbxUsername.Text;
                string password = tbxPassword.Text;

                var result = UsersDAL.Login(username, password);
                if (result.Code < 0 || result.DbReturnValue < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;
                    return;
                }

                if (result.Data.Tables.Count == 0 ||
                    result.Data.Tables[0].Rows.Count == 0)
                {
                    lbError.InnerText = "Sai tên đăng nhập hoặc mật khẩu không đúng.";
                    lbError.Visible = true;
                    return;
                }

                UserBOL BOL = new UserBOL(result.Data.Tables[0].Rows[0]);
                Utilities.SaveLoggedInfo(BOL);
                //---
                GoToHome();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteError("[Default][btnLogin_Click] Exception: {0}", ex.ToString());
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        private void GoToHome()
        {
            Response.Redirect("Introductions.aspx", false);
        }
    }
}