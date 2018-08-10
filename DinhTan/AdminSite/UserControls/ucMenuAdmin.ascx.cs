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
    public partial class ucMenuAdmin : System.Web.UI.UserControl
    {
        private const string __tag = "[ucMenuAdmin]";
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
                //---
                if (!string.IsNullOrEmpty(menu.RedirectUrl) && menu.RedirectUrl.Contains("About.aspx"))
                {
                    li.InnerHtml = string.Format("<a href='{0}'>{1}</a>",
                        "Introductions.aspx",
                        menu.Name_VN);
                }
                else
                {
                    li.InnerHtml = string.Format("<a href='{0}'>{1}</a>",
                        menu.RedirectUrl,
                        menu.Name_VN);
                }

                StartAddMenuItem(li, menu.Id, result.Data.Tables[0].Rows);

                this.ulMenuAdmin.Controls.Add(li);
            }
            //---
            StartAddMenuOther();
        }

        private void StartAddMenuOther()
        {
            string tag = __tag + "[StartAddMenuOther]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                HtmlGenericControl li = new HtmlGenericControl("li");
                li.InnerHtml = string.Format("<a href='{0}'>{1}</a>",
                    "Other.aspx",
                    "Other");

                var result = OtherTypeDAL.GetAll();
                if (result.Code >= 0 && result.Data.Tables.Count > 0)
                {
                    HtmlGenericControl ul = new HtmlGenericControl("ul");

                    foreach (DataRow row in result.Data.Tables[0].Rows)
                    {
                        OtherTypeBOL otherType = new OtherTypeBOL(row);
                        if (otherType == null)
                        {
                            continue;
                        }

                        HtmlGenericControl subLi = new HtmlGenericControl("li");
                        subLi.InnerHtml = string.Format("<a href='{0}?CatId={1}'>{2}</a>",
                            "OtherDetail.aspx",
                            otherType.Id,
                            otherType.Name_VN);

                        ul.Controls.Add(subLi);
                    }

                    if (ul.Controls.Count > 0)
                    {
                        li.Controls.Add(ul);
                    }
                }

                this.ulMenuAdmin.Controls.Add(li);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End.");
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