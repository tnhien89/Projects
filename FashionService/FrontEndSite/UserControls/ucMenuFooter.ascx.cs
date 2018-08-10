using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FrontEndSite.BusinessObject;

namespace FrontEndSite.UserControls
{
    public partial class ucMenuFooter : System.Web.UI.UserControl
    {
        private const string __tag = "ucMenuFooter";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void CreateMenuFooter(List<MenuBOL> list)
        {
            string tag = __tag + "[CreateMenuFooter]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                if (list == null)
                {
                    LogHelpers.WriteStatus(tag, "list = null");
                    return;
                }

                foreach (MenuBOL menu in list)
                {
                    if (menu.ParentId > 0)
                    {
                        continue;
                    }

                    HtmlGenericControl li = new HtmlGenericControl("li");
                    li.InnerHtml = string.Format("<a href='{0}'>{1}</a>",
                        menu.RedirectUrl,
                        Utilities.IsLangueEN() ? menu.Name_EN : menu.Name_VN);

                    if (menu.Type == "LienHe")
                    {
                        StartAddSubMenuContact(li, menu.Id);
                        this.ulMenuFooter.Controls.Add(li);

                        continue;
                    }

                    HtmlGenericControl subUL = new HtmlGenericControl("ul");
                    foreach (MenuBOL subMenu in list)
                    {
                        if (subMenu.ParentId == menu.Id)
                        {
                            HtmlGenericControl subLI = new HtmlGenericControl("li");
                            subLI.InnerHtml = string.Format("<a href='{0}'>{1}</a>",
                                menu.RedirectUrl,
                                Utilities.IsLangueEN() ? subMenu.Name_EN : subMenu.Name_VN);

                            subUL.Controls.Add(subLI);
                        }
                    }

                    if (subUL.Controls.Count > 0)
                    {
                        li.Controls.Add(subUL);
                    }

                    this.ulMenuFooter.Controls.Add(li);
                }
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
                    Utilities.IsLangueEN() ? "Contact info" : "Thông tin liên hệ");
                //---
                HtmlGenericControl liForm = new HtmlGenericControl("li");
                liForm.InnerHtml = string.Format("<a href='{0}'>{1}</a>",
                    "ContactForm.aspx",
                    Utilities.IsLangueEN() ? "Contact form" : "Form liên hệ");
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
    }
}