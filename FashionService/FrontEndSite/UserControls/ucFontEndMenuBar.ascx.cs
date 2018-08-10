using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FrontEndSite.BusinessObject;
using FrontEndSite.DataAccess;

namespace FrontEndSite.UserControls
{
    public partial class ucFontEndMenuBar : System.Web.UI.UserControl
    {
        private const string __tag = "[ucFontEndMenuBar]";
        private string _parentUrl;
        private List<MenuBOL> _menuFooter;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public List<MenuBOL> CreateMenuBar()
        {
            //---
            HtmlGenericControl liHome = new HtmlGenericControl("li");
            string nameHome = Utilities.IsLangueEN() ? "Home" : "Trang Chủ";
            liHome.InnerHtml = string.Format("<a href='{0}'>{1}</a>",
                "Home.aspx",
                nameHome);
            //---
            ulMenuBar.Controls.Add(liHome);
            //----
            var result = MenuDAL.GetAll();
            if (result.Code < 0 || result.Data == null || result.Data.Tables.Count == 0)
            {
                return null;
            }

            _menuFooter = new List<MenuBOL>();

            foreach (DataRow row in result.Data.Tables[0].Rows)
            {
                MenuBOL menu = new MenuBOL(row);
                if (menu.ParentId > 0)
                {
                    continue;
                }

                this._menuFooter.Add(menu);
                //---
                _parentUrl = menu.RedirectUrl;

                HtmlGenericControl li = new HtmlGenericControl("li");
                //--
                if (menu.Childrens > 0 || menu.Type == "DuAn")
                {
                    li.InnerHtml = string.Format("<a href='{0}'>{1}</a>",
                        menu.RedirectUrl,
                        Utilities.IsLangueEN() ? menu.Name_EN : menu.Name_VN);
                }
                else
                {
                    li.InnerHtml = string.Format("<a href='#'>{0}</a>",
                        Utilities.IsLangueEN() ? menu.Name_EN : menu.Name_VN);
                }

                if (menu.Type == "GioiThieu")
                {
                    StartAddSubItemAbout(li, menu.Id);
                }
                else if (menu.Type == "LienHe")
                {
                    StartAddSubMenuContact(li, menu.Id);
                }
                else
                {
                    StartAddSubMenuItem(li, menu.Id, result.Data.Tables[0].Rows);
                }

                this.ulMenuBar.Controls.Add(li);
            }

            return _menuFooter;
        }

        private void StartAddSubMenuContact(HtmlGenericControl li, int menuId)
        {
            string tag = __tag + "[StartAddSubMenuContact]";
            LogHelpers.WriteStatus(tag, "Start....");

            try
            {
                HtmlGenericControl ul = new HtmlGenericControl("ul");
                HtmlGenericControl liInfo = new HtmlGenericControl("li");
                liInfo.InnerHtml = string.Format("<a href='{0}'>{1}</a>",
                    "ContactInfo.aspx",
                    Utilities.IsLangueEN() ? "Company infomation" : "Địa chỉ liên lạc");
                //---
                HtmlGenericControl liForm = new HtmlGenericControl("li");
                liForm.InnerHtml = string.Format("<a href='{0}'>{1}</a>",
                    "ContactForm.aspx",
                    Utilities.IsLangueEN() ? "Send contact" : "Gởi liên hệ");
                //---
                ul.Controls.Add(liInfo);
                ul.Controls.Add(liForm);
                //---
                li.Controls.Add(ul);
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

        private void StartAddSubItemAbout(HtmlGenericControl li, int menuId)
        {
            string tag = __tag + "[StartAddSubItemAbout]";

            if (menuId <= 0)
            {
                return;
            }

            var result = NewsDAL.GetAll(menuId);
            if (result.Code < 0)
            {
                LogHelpers.WriteError(tag, result.ErrorMessage);
                return;
            }

            if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count < 0)
            {
                return;
            }

            HtmlGenericControl ul = new HtmlGenericControl("ul");

            foreach (DataRow row in result.Data.Tables[0].Rows)
            {
                try
                {
                    NewsBOL news = new NewsBOL(row);
                    //---
                    HtmlGenericControl subLi = new HtmlGenericControl("li");
                    subLi.InnerHtml = string.Format("<a href='{0}#{1}'>{2}</a>",
                        Utilities.GetNavigationUrl("AboutPage"),
                        news.Id,
                        Utilities.IsLangueEN() ? news.Name_EN : news.Name_VN);
                    //----
                    ul.Controls.Add(subLi);
                }
                catch (Exception ex)
                {
                    LogHelpers.WriteException(tag, ex.ToString());
                }
            }

            if (ul.Controls.Count > 0)
            {
                li.Controls.Add(ul);
            }
        }

        private void StartAddSubMenuItem(HtmlGenericControl ctrlRoot, int parentId, DataRowCollection rows)
        {
            HtmlGenericControl ul = new HtmlGenericControl("ul");

            foreach (DataRow row in rows)
            {
                MenuBOL menu = new MenuBOL(row);
                if (menu.ParentId == parentId)
                {
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    if (menu.Childrens > 0)
                    {
                        li.InnerHtml = string.Format("<a href='{0}?MenuId={1}'>{2}</a>",
                            _parentUrl,
                            menu.Id,
                            Utilities.IsLangueEN() ? menu.Name_EN : menu.Name_VN);
                    }
                    else
                    {
                        li.InnerHtml = string.Format("<a href='#'>{0}</a>",
                        Utilities.IsLangueEN() ? menu.Name_EN : menu.Name_VN);
                    }
                    //---
                    ul.Controls.Add(li);

                    StartAddSubMenuItem(li, menu.Id, rows);

                    _menuFooter.Add(menu);
                }
            }

            if (ul.Controls.Count > 0)
            {
                ctrlRoot.Controls.Add(ul);
            }
        }
    }
}