using AdminSite.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminSite
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        private const string __type = "ThuVienHinh";
        private int _menuId;
        protected void Page_Load(object sender, EventArgs e)
        {
            _menuId = Utilities.GetRequestParameter("MenuId");
            if (_menuId <= 0)
                _menuId = MenuDAL.GetId(__type);

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
                        this.ucGalleryItems.StartDeleteProjects();
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

            ucGalleryItems.StartBindingData(menuId);
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