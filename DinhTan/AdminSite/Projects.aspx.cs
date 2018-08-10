using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class Projects : System.Web.UI.Page
    {
        private int _menuId;
        private string _currentUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            Utilities.GetMenuIdAndUrl("DuAn", out _menuId, out _currentUrl);
            //---
            if (!IsPostBack)
            {
                StartLoadMenuDetail(_menuId);
            }

            if (IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request["__EVENTARGUMENT"]) && Request["__EVENTARGUMENT"] == "DeleteProjects")
                {
                    ucProjectItems.StartDeleteProjects();
                }
            }
            //----
            StartBindingData();
        }

        private void StartLoadMenuDetail(int id)
        {
            if (id <= 0)
            {
                return;
            }

            ucMenuDetail.StartLoadMenuDetail(id);
            ucMenuDetail.StartBindingSubMenu(id);
        }

        private void StartBindingData()
        {
            if (_menuId < 0)
            {
                return;
            }

            
            ucProjectItems.StartBindingData(_menuId);
        }
    }
}