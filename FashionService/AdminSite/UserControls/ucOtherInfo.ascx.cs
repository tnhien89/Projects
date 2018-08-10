using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSite.BusinessObject;
using AdminSite.DataAccess;
using NLog;

namespace AdminSite.UserControls
{
    public partial class ucOtherInfo : System.Web.UI.UserControl
    {
        private const string __tag = "[ucOtherInfo]";
        private Logger _log = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            //btnSubmit.Text = "Add";

            if (!IsPostBack)
            {
                int id = Utilities.GetRequestParameter("GroupId");
                if (id > 0)
                {
                    formSubMenu.Visible = false;
                    StartLoadOtherGroupInfo(id);
                }
                else
                {
                    id = Utilities.GetRequestParameter("Id");
                    if (id > 0)
                    {
                        StartBindingGroupOther(id);
                        StartLoadOtherInfo(id);
                    }
                }
            }
        }

        private void StartLoadOtherGroupInfo(int id)
        {
#if DEBUG
            _log.Debug("id: {0}", id);
#endif
            lbError.Visible = false;

            try
            {
                var result = OtherDAL.Get(id);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;
                    return;
                }

                OtherBOL otherType = new OtherBOL(result.Data.Tables[0].Rows[0]);
                hfId.Value = otherType.Id.ToString();
                tbxNameVN.Text = otherType.Name_VN;
                tbxNameEN.Text = otherType.Name_EN;
                tbxDesVN.Text = otherType.Description_VN;
                tbxDesEN.Text = otherType.Description_EN;

                tbxNameEN.Enabled = true;
                tbxNameVN.Enabled = true;
                //---
                //btnSubmit.Text = "Update";
                //btnCancel.Visible = true;
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        public void StartBindingGroupOther(int id)
        {
            //formSubMenu.Visible = false;
            if (id <= 0)
            {
                return;
            }

            var result = OtherDAL.GetAllGroup(id);
            if (result.Code < 0)
            {
                lbError.InnerText = result.ErrorMessage;
                lbError.Visible = true;

                return;
            }

            lbError.Visible = false;

            try
            {
                ddlSubMenu.DataSource = result.Data;
                ddlSubMenu.DataValueField = "Id";
                ddlSubMenu.DataTextField = "Name_VN";
                //---
                ddlSubMenu.DataBind();
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        private void StartLoadOtherInfo(int id)
        {
            string tag = __tag + "[StartLoadOtherInfo]";
            LogHelpers.WriteStatus(tag, "Start...");

            formSubMenu.Visible = false;
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
                btnAddSubMenu.Attributes.Add("data-id", otherType.Id.ToString());

                if (!otherType.Name_VN.Contains("Banner"))
                {
                    formSubMenu.Visible = true;
                }

                tbxNameEN.Enabled = false;
                tbxNameVN.Enabled = false;
                
                //---
                //btnSubmit.Text = "Update";
                //btnCancel.Visible = true;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
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
                if (tbxNameVN.Enabled)
                {
                    UpdateOtherProcess();
                    return;
                }

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
        }

        private void UpdateOtherProcess()
        {
            OtherBOL other = new OtherBOL()
            {
                Id = string.IsNullOrEmpty(hfId.Value) ? 0 : int.Parse(hfId.Value),
                Name_VN = tbxNameVN.Text,
                Name_EN = tbxNameEN.Text,
                Description_VN = tbxDesVN.Text,
                Description_EN = tbxDesEN.Text,
                IsGroup = true,
                InsertDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            var result = OtherDAL.InsertOrUpdate(other);
            if (result.Code < 0)
            {
                lbError.InnerText = result.ErrorMessage;
                lbError.Visible = true;

                return;
            }

            StartClearData();

            Response.Redirect(Request.RawUrl, false);
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
            //btnCancel.Visible = false;
        }
    }
}