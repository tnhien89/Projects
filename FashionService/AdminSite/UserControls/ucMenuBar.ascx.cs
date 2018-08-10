using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AdminSite.BusinessObject;
using AdminSite.DataAccess;

namespace AdminSite.UserControls
{
    public partial class ucMenuBar : System.Web.UI.UserControl
    {
        private string _parentUrl;

        protected void Page_Load(object sender, EventArgs e)
        {
            CreateMenuBar();
        }

        private void CreateMenuBar()
        {
            var result = MenuDAL.GetAll();
            if (result.Code < 0 || result.Data == null || result.Data.Tables.Count == 0)
            {
                return;
            }

            foreach (DataRow row in result.Data.Tables[0].Rows)
            {
                MenuBOL menu = new MenuBOL(row);
                if (menu.ParentId > 0)
                {
                    continue;
                }
                //---
                _parentUrl = menu.RedirectUrl;

                HtmlGenericControl li = new HtmlGenericControl("li");
                li.InnerHtml = string.Format("<a href='{0}'>{1}</a>", menu.RedirectUrl, menu.Name_VN);

                StartAddMenuItem(li, menu.Id, result.Data.Tables[0].Rows);

                this.ulMenuBar.Controls.Add(li);
            }
        }

        private void StartAddMenuItem(HtmlGenericControl ctrlRoot, int parentId, DataRowCollection rows)
        {
            HtmlGenericControl ul = new HtmlGenericControl("ul");

            foreach (DataRow row in rows)
            {
                MenuBOL menu = new MenuBOL(row);
                if (menu.ParentId == parentId)
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    li.InnerHtml = string.Format("<a href='{0}?MenuId={1}'>{2}</a>", _parentUrl, menu.Id, menu.Name_VN);
                    //---
                    ul.Controls.Add(li);

                    StartAddMenuItem(li, menu.Id, rows);
                }
            }

            if (ul.Controls.Count > 0)
            {
                ctrlRoot.Controls.Add(ul);
            }
        }

    }
}