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
    public partial class ucMenuDetail : System.Web.UI.UserControl
    {
        private string _tag = "[ucMenuDetail]";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void StartBindingSubMenu(int id)
        {
            string tag = _tag + "[StartBindingSubMenu]";
            if (id <= 0)
            {
                return;
            }

            var result = MenuDAL.GetAllSub(id);
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
                LogHelpers.WriteException(tag, ex.ToString());
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        public void StartLoadMenuDetail(int id)
        {
            string tag = _tag + "[StartLoadMenuDetail]";

            if (id <= 0)
            {
                return;
            }

            var result = MenuDAL.Get(id);
            if (result.Code < 0)
            {
                lbError.InnerText = result.ErrorMessage;
                lbError.Visible = true;

                return;
            }

            lbError.Visible = false;
            hfMenuId.Value = id.ToString();
            //---
            try
            {
                MenuBOL menu = new MenuBOL(result.Data.Tables[0].Rows[0]);
                btnAddSubMenu.Attributes.Add("data-id", menu.Id.ToString());
                tbxNameVN.Text = menu.Name_VN;
                tbxDesVN.Text = menu.Description_VN;
                tbxNameEN.Text = menu.Name_EN;
                tbxDesEN.Text = menu.Description_EN;
                //---
                //if (menu.ParentId <= 0)
                //{
                //    tbxNameVN.Enabled = false;
                //    tbxNameEN.Enabled = false;
                //}
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //---
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        public void ShowSubMenu(bool show)
        {
            formSubMenu.Visible = show;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lbError.Visible = false;
            UpdateMenuProcessing();
        }

        private void UpdateMenuProcessing()
        {
            string tag = _tag + "[UpdateMenuProcessing]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                MenuBOL menu = new MenuBOL() { 
                    Id = string.IsNullOrEmpty(hfMenuId.Value) ? 0 : int.Parse(hfMenuId.Value),
                    Name_VN = tbxNameVN.Text,
                    Name_EN = tbxNameEN.Text,
                    Description_VN = tbxDesVN.Text,
                    Description_EN = tbxDesEN.Text,
                    InsertDate = DateTime.Now,
                    UpdatedDate = DateTime.Now
                };

                var result = MenuDAL.InsertOrUpdate(menu);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;

                    return;
                }
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
    }
}