using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSite.BusinessObject;
using AdminSite.DataAccess;

namespace AdminSite.UserControls
{
    public partial class ucOtherInfo : System.Web.UI.UserControl
    {
        private const string __tag = "[ucOtherInfo]";

        protected void Page_Load(object sender, EventArgs e)
        {
            btnSubmit.Text = "Add";

            if (!IsPostBack)
            {
                int id = Utilities.GetRequestParameter("Id");
                if (id > 0)
                {
                    StartLoadOtherInfo(id);
                }
            }
        }

        private void StartLoadOtherInfo(int id)
        {
            string tag = __tag + "[StartLoadOtherInfo]";
            LogHelpers.WriteStatus(tag, "Start...");

            lbError.Visible = false;

            try
            {
                var result = OtherTypeDAL.Get(id);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;
                    return;
                }

                OtherTypeBOL otherType = new OtherTypeBOL(result.Data.Tables[0].Rows[0]);
                hfId.Value = otherType.Id.ToString();
                tbxNameVN.Text = otherType.Name_VN;
                tbxNameEN.Text = otherType.Name_EN;
                tbxDesVN.Text = otherType.Description_VN;
                tbxDesEN.Text = otherType.Description_EN;

                if (otherType.Name_VN == "Banner")
                {
                    tbxNameEN.Enabled = false;
                    tbxNameVN.Enabled = false;
                }
                else
                {
                    tbxNameVN.Enabled = true;
                    tbxNameEN.Enabled = true;
                }
                //---
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
            InsertOrUpdateBannerProcess();
        }

        private void InsertOrUpdateBannerProcess()
        {
            string tag = __tag + "[InsertOrUpdateBannerProcess]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {

                OtherTypeBOL other = new OtherTypeBOL()
                {
                    Id = string.IsNullOrEmpty(hfId.Value) ? 0 : int.Parse(hfId.Value),
                    Name_VN = tbxNameVN.Text,
                    Name_EN = tbxNameEN.Text,
                    Description_VN = tbxDesVN.Text,
                    Description_EN = tbxDesEN.Text,
                    InsertDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };

                var result = OtherTypeDAL.InsertOrUpdate(other);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;

                    return;
                }

                StartClearData();

                Response.Redirect(Request.RawUrl, false);
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

        private void StartClearData()
        {
            hfId.Value = "";
            tbxNameVN.Text = "";
            tbxNameEN.Text = "";
            tbxDesVN.Text = "";
            tbxDesEN.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            StartClearData();
            btnCancel.Visible = false;
        }
    }
}