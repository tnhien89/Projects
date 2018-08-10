using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrontEndSite.BusinessObject;
using FrontEndSite.DataAccess;

namespace FrontEndSite.UserControls
{
    public partial class ucNewsDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void StartShowNewsDetail(NewsBOL news)
        {
            string tag = "[ucNewsDetail][StartShowNewsDetail]";

            if (news == null)
            {
                return;
            }

            try
            {
                bool langueEn = Utilities.IsLangueEN();

                lbUpdatedDate.InnerText = news.UpdatedDate.ToString("MM/dd/yyyy HH:mm zzz");
                lbTitle.InnerText = Utilities.IsLangueEN() ? news.Name_EN : news.Name_VN;
                ltrContent.Text = Utilities.SetFullLinkImage(langueEn ? news.Content_EN : news.Content_VN,
                    Utilities.GetDirectory("ImageNewsDir"));
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        public void StartShowNewsDetail(int newsId)
        {
            string tag = "[ucNewsDetail][StartShowNewsDetail]";

            if (newsId <= 0)
            {
                return;
            }

            var result = NewsDAL.Get(newsId);
            if (result.Code < 0)
            {
                lbError.InnerText = result.ErrorMessage;
                lbError.Visible = true;

                return;
            }

            lbError.Visible = false;
            try
            {
                bool langueEn = Utilities.IsLangueEN();
                //--
                NewsBOL news = new NewsBOL(result.Data.Tables[0].Rows[0]);
                lbUpdatedDate.InnerText = news.UpdatedDate.ToString("MM/dd/yyyy HH:mm zzz");
                lbTitle.InnerText = langueEn ? news.Name_EN : news.Name_VN;
                ltrContent.Text = Utilities.SetFullLinkImage(langueEn ? news.Content_EN : news.Content_VN,
                    Utilities.GetDirectory("ImageNewsDir"));
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }
    }
}