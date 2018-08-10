using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FrontEndSite.UserControls
{
    public partial class ucGold : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //using (WebClient client = new WebClient())
            //{

            //    string htmlCode = client.DownloadString("http://www.eximbank.com.vn/WebsiteExrate/Gold_vn_2012.aspx");

            //    foreach (Match match in Regex.Matches(htmlCode,
            //    "<span id='GoldRateRepeater_lblCSHBUYRT_0'>(.*?)</span>",
            //        RegexOptions.IgnoreCase))
            //    {
            //        sbjBuy.InnerText = match.Groups[1].Value; // in kết quả giá vàng

            //    }

            //}
        }
    }
}