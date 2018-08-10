using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrontEndSite.EN
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
            //---
            StartLoadProjectsList();
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