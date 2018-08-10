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
    public partial class ucNewsMain : System.Web.UI.UserControl
    {
        private const string __tag = "[ucNewsMain]";

        protected void Page_Load(object sender, EventArgs e)
        {
            lbHeader.InnerText = Utilities.IsLangueEN() ? "News" : "Tin Tức";
        }

        public void StartBindingDataNews(int menuId, int id, string navigationUrl, string header)
        {
            string tag = __tag + "[StartBindingDataNews]";
            //--
            lbHeader.InnerText = header;

            if (menuId <= 0)
            {
                return;
            }

            try
            {
                var result = NewsDAL.GetAll(menuId);
                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;
                    //---
                    LogHelpers.WriteError(tag, result.ErrorMessage);
                    return;
                }

                if (result.Data.Tables.Count == 0 || result.Data.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                ucNewsList.StartBindingData(result.Data, menuId, navigationUrl);
                //--
                if (id <= 0)
                {
                    NewsBOL news = new NewsBOL(result.Data.Tables[0].Rows[0]);
                    lbHeader.InnerText = Utilities.IsLangueEN() ? news.Name_EN : news.Name_VN;
                    //--
                    ucNewsDetail.StartShowNewsDetail(news);
                }
                else
                {
                    StartLoadNewsDetail(id);
                }
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //--
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        private void StartLoadNewsDetail(int id)
        {
            string tag = __tag + "[StartShowNewsDetail]";

            if (id <= 0)
            {
                return;
            }

            var result = NewsDAL.Get(id);
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
                lbHeader.InnerText = langueEn ? news.Name_EN : news.Name_VN;
                //--
                ucNewsDetail.StartShowNewsDetail(news);
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