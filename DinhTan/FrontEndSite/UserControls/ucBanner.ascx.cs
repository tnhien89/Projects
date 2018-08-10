using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using FrontEndSite.BusinessObject;
using FrontEndSite.DataAccess;

namespace FrontEndSite.UserControls
{
    public partial class ucBanner : System.Web.UI.UserControl
    {
        private const string __tag = "[ucBanner]";

        protected void Page_Load(object sender, EventArgs e)
        {
            StartBindingBanners();
        }

        private void StartBindingBanners()
        {
            string tag = __tag + "[StartBindingBanners]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                var result = OtherDAL.GetAllBanners();
                if (result.Code < 0)
                {
                    return;
                }

                for (int i = 0; i < result.Data.Tables[0].Rows.Count; i++)
                {
                    OtherBOL other = new OtherBOL(result.Data.Tables[0].Rows[i]);
                    //----
                    HtmlGenericControl li = new HtmlGenericControl("li");
                    li.Attributes.Add("data-target", "#banner-carousel");
                    li.Attributes.Add("data-slide-to", i.ToString());
                    if (i == 0)
                    {
                        li.Attributes.Add("class", "active");
                    }

                    olbanners.Controls.Add(li);
                    //---
                    string imgUrl = Path.Combine(Utilities.GetDirectory("ImageOtherDir"), other.ImageLink);
                    //--
                    HtmlGenericControl divImage = new HtmlGenericControl("div");
                    if (i == 0)
                    {
                        divImage.Attributes.Add("class", "item active");
                    }
                    else
                    {
                        divImage.Attributes.Add("class", "item");
                    }
                    //---
                    HtmlGenericControl img = new HtmlGenericControl("img");
                    img.Attributes.Add("src", imgUrl);

                    HtmlGenericControl divBannerTitle = new HtmlGenericControl("div");
                    divBannerTitle.Attributes.Add("class", "banner-title");
                    HtmlGenericControl divBannerTitleBackground = new HtmlGenericControl("div");
                    divBannerTitleBackground.Attributes.Add("class", "banner-title-background");
                    HtmlGenericControl hplBanner = new HtmlGenericControl("a");
                    hplBanner.InnerText = other.Name_VN;
                    hplBanner.Attributes.Add("href", other.Link);

                    divBannerTitle.Controls.Add(divBannerTitleBackground);
                    divBannerTitle.Controls.Add(hplBanner);
                    
                    divImage.Controls.Add(img);
                    divImage.Controls.Add(divBannerTitle);

                    BannerCarouselInner.Controls.Add(divImage);
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
    }
}