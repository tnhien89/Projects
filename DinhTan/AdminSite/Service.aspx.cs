using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class Service : System.Web.UI.Page
    {
        private int _menuId;

        protected void Page_Load(object sender, EventArgs e)
        {
            Utilities.GetMenuIdAndUrl("DichVu", out _menuId);

            if (!IsPostBack)
            {
                StartLoadMenuDetail(_menuId);
            }
            else
            {
                if (!string.IsNullOrEmpty(Request["__EVENTARGUMENT"]))
                {
                    if (Request["__EVENTARGUMENT"] == "DeleteNewsItems")
                    {
                        ucNewsItems.StartDeleteItems();
                    }
                }
            }

            StartBindingNewsItems(_menuId);
        }

        private void StartBindingNewsItems(int menuId)
        {
            if (menuId <= 0)
            {
                return;
            }

            ucNewsItems.StartBindingData(menuId, Utilities.GetNavigationUrl("ServiceDetail"));
        }

        private void StartLoadMenuDetail(int menuId)
        {
            if (menuId <= 0)
            {
                return;
            }

            ucMenuDetail.StartBindingSubMenu(menuId);
            ucMenuDetail.StartLoadMenuDetail(menuId);
        }
    }
}