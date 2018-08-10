using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSite.BusinessObject;
using AdminSite.DataAccess;

namespace AdminSite.UserControls
{
    public partial class ucUserInfo : System.Web.UI.UserControl
    {
        private const string __tag = "[ucUserInfo]";

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSubmit.Text = "Add";
            btnCancel.Visible = false;

            if (!IsPostBack)
            {
                ddlGender.Items.Add(new ListItem()
                {
                    Value = "0",
                    Text = "Nam",
                    Selected = true
                });

                ddlGender.Items.Add(new ListItem() { 
                    Value = "1",
                    Text = "Nữ"
                });

                ddlGender.DataBind();

                int id = Utilities.GetRequestParameter("Id");
                if (id > 0)
                {
                    StartLoadUserInfo(id);
                }
            }

            
        }

        private void StartLoadUserInfo(int id)
        {
            string tag = __tag + "[StartLoadUserInfo]";
            LogHelpers.WriteStatus(tag, "Id = " + id.ToString(), "Start...");

            try
            {
                var result = UsersDAL.Get(id);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;
                    LogHelpers.WriteError(tag, result.ErrorMessage);

                    return;
                }

                UserBOL user = new UserBOL(result.Data.Tables[0].Rows[0]);
                hfId.Value = user.Id.ToString();
                tbxFirstNameVN.Text = user.FirstName_VN;
                tbxFirstNameEN.Text = user.FirstName_EN;
                tbxLastNameVN.Text = user.LastName_VN;
                tbxLastNameEN.Text = user.LastName_EN;
                tbxUsername.Text = user.Username;
                tbxEmail.Text = user.Email;
                tbxPhone.Text = user.Phone;
                tbxDateOfBirth.Text = user.DateOfBirth.ToString("yyyy-MM-dd");
                ddlGender.SelectedValue = user.Gender.Trim() == "Nam" ? "0" : "1";

                lbError.Visible = false;
                btnSubmit.Text = "Update";
                btnCancel.Visible = true;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());

                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End.");
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lbError.Visible = false;
            InsertOrUpdateUserProcess();
        }

        private void InsertOrUpdateUserProcess()
        {
            string tag = __tag + "[InsertOrUpdateUserProcess]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                UserBOL user = new UserBOL() { 
                    Id = string.IsNullOrEmpty(hfId.Value) ? 0 : int.Parse(hfId.Value),
                    FirstName_VN = tbxFirstNameVN.Text,
                    FirstName_EN = tbxFirstNameEN.Text,
                    LastName_VN = tbxLastNameVN.Text,
                    LastName_EN = tbxLastNameEN.Text,
                    Username = tbxUsername.Text,
                    Password = Utilities.EncryptPassword(tbxPassword.Text),
                    Email = tbxEmail.Text,
                    Phone = tbxPhone.Text,
                    DateOfBirth = Utilities.ToDateTime(tbxDateOfBirth.Text),
                    Gender = ddlGender.SelectedValue == "0" ? "Nam" : "Nữ",
                    InsertDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };

                var result = UsersDAL.InsertOrUpdate(user);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;

                    return;
                }

                Response.Redirect("~/Introductions.aspx", false);
                //string url = Request.RawUrl;
                //Response.Redirect(url, false);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //---
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End.");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string url = Request.RawUrl;
            Response.Redirect(url, false);
        }
    }
}