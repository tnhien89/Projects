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
    public partial class WebForm2 : System.Web.UI.Page
    {
        private int _menuId;
        private string _currentUrl;

        protected void Page_Load(object sender, EventArgs e)
        {

            Utilities.GetMenuIdAndUrl("GioiThieu", out _menuId, out _currentUrl);
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

            StartBindingData();
        }

        private void StartLoadMenuDetail(int menuId)
        {
            if (menuId <= 0)
            {
                return;
            }

            ucMenuDetail.StartLoadMenuDetail(menuId);
            ucMenuDetail.ShowSubMenu(false);
            //ucMenuDetail.StartBindingSubMenu(menuId);
        }

        void ucMenuItems_pageChanging()
        {
            StartBindingData();
        }


        private void StartBindingData()
        {
            if (_menuId < 0)
            {
                return;
            }

            StartBindingNewsItems(_menuId);
        }

        private void StartBindingNewsItems(int menuId)
        {
            ucNewsItems.HideErrorMessage();
            ucNewsItems.StartBindingData(menuId, Utilities.GetNavigationUrl("IntroDetail"));
        }
    }
}