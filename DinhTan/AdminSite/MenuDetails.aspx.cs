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
    public partial class WebForm3 : System.Web.UI.Page
    {
        private int _menuId;
        private int _parentId;

        private string _tag = "[MenuDetails.aspx.cs]";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    ViewState["GoBackUrl"] = Request.UrlReferrer.ToString();
                }
                //-----
                if (Request["MenuId"] == null && Request["ParentId"] == null)
                {
                    GoBackPage();
                }

                if (Request["MenuId"] != null)
                {
                    if (!int.TryParse(Request["MenuId"], out _menuId))
                    {
                        return;
                    }

                    _menuId = Utilities.GetRequestParameter("MenuId");

                    //---
                    StartLoadMenuInfo(_menuId);

                    return;
                }
            }
            else
            {
                _menuId = Utilities.GetRequestParameter("MenuId");
                _parentId = Utilities.GetRequestParameter("ParentId");

                if (!string.IsNullOrEmpty(Request["__EVENTARGUMENT"]) && Request["__EVENTARGUMENT"] == "GoBackPage")
                {
                    GoBackPage();
                }
            }
        }

        private void StartLoadMenuInfo(int id)
        {
            if (id <= 0)
            {
                return;
            }

            var result = MenuDAL.Get(id);
            if (result.Code < 0)
            {
                return;
            }

            MenuBOL menuBOL = new MenuBOL(result.Data.Tables[0].Rows[0]);
            tbxNameVN.Text = menuBOL.Name_VN;
            tbxDesVN.Text = menuBOL.Description_VN;
            tbxNameEN.Text = menuBOL.Name_EN;
            tbxDesEN.Text = menuBOL.Description_EN;
            //---
            if (menuBOL.ParentId > 0)
            {
                _parentId = menuBOL.ParentId;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string tag = _tag + "[btnSubmit_Click]";

            MenuBOL obj = new MenuBOL() { 
                Name_VN = tbxNameVN.Text,
                Description_VN = tbxDesVN.Text,
                Name_EN = tbxNameEN.Text,
                Description_EN = tbxDesEN.Text,
                InsertDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };

            if (_menuId > 0)
            {
                obj.Id = _menuId;
            }
            else if (_parentId > 0)
            {
                obj.ParentId = _parentId;
            }

            var result = MenuDAL.InsertOrUpdate(obj);
            if (result.Code < 0)
            {
                LogHelpers.WriteError(tag, result.ErrorMessage);
            }
            else
            {
                GoBackPage();
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