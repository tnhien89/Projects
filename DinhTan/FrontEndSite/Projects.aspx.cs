using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrontEndSite.DataAccess;

namespace FrontEndSite
{
    public partial class Projects : System.Web.UI.Page
    {
        private int _menuId;
        private const string __navigationUrl = "ProjectsInfo.aspx";
        //--
        private const string __tag = "[Projects]";

        protected void Page_Load(object sender, EventArgs e)
        {
            
            ucProjectsList.Visible = true;
            _menuId = Utilities.GetMenuId("DuAn");
            //---
            if (!IsPostBack)
            {
                StartGetMenuName(_menuId);
            }
            //---
            StartLoadProjectsList();
        }

        private void StartGetMenuName(int _menuId)
        {
            string tag = __tag + "[StartGetMenuName]";
            LogHelpers.WriteStatus(tag, "menuId: " + _menuId.ToString(), "Start...");

            try
            {
                var result = MenuDAL.GetMenuName(_menuId);
                if (result.Code < 0)
                {
                    LogHelpers.WriteError(tag, result.ErrorMessage);

                    return;
                }

                lbContentHeader.InnerText = result.Data.ToString();
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.Message);
            }
        }

        private void StartLoadProjectsList()
        {
            if (_menuId <= 0)
            {
                return;
            }

            bool isLoadAll = string.IsNullOrEmpty(Request["MenuId"]);

            ucProjectsList.StartBindingProjects(_menuId, __navigationUrl, isLoadAll);
        }
    }
}