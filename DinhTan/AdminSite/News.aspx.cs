using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class News : System.Web.UI.Page
    {
        private int _menuId;
        private string _currentUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            Utilities.GetMenuIdAndUrl("TinTuc", out _menuId, out _currentUrl);
            //----
            if (!IsPostBack)
            {
                StartLoadSubMenu();
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
            //---
            StartBindingData();
        }

        private void StartLoadSubMenu()
        {
            if (_menuId < 0)
            {
                return;
            }

            ucMenuDetail.StartLoadMenuDetail(_menuId);
            ucMenuDetail.StartBindingSubMenu(_menuId);
        }

        private void StartBindingData()
        {
            if (_menuId < 0)
            {
                return;
            }

            
            ucNewsItems.StartBindingData(_menuId, Utilities.GetNavigationUrl("NewsDetail"));
        }
    }
}