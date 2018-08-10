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
    public partial class ucContactInfo : System.Web.UI.UserControl
    {
        private const string __tag = "[ucContactInfo]";

        protected void Page_Load(object sender, EventArgs e)
        {
            lbContentHeader.InnerText = Utilities.IsLangueEN() ? "Contact Info" : "Thông tin liên hệ";
            lbGoogleMapsHeader.InnerText = Utilities.IsLangueEN() ? "Road map" : "Bản đồ đường đi";
            StartLoadContactInfo();
        }

        private void StartLoadContactInfo()
        {
            string tag = __tag + "[StartLoadContactInfo]";
            LogHelpers.WriteStatus(tag, "Start...");

            try
            {
                var result = AboutsDAL.GetAll();
                if (result.Code < 0)
                {
                    LogHelpers.WriteError(tag, result.ErrorMessage);
                }

                AboutBOL about = new AboutBOL(result.Data.Tables[0].Rows[0]);
                if (Utilities.IsLangueEN())
                {
                    lbName.InnerText = about.Name_EN;
                    lbAddress.InnerText = "Address:";
                    lbAddressInfo.InnerText = about.Address_EN;
                    lbPhone.InnerText = "Phone:";
                }
                else
                {
                    lbName.InnerText = about.Name_VN;
                    lbAddress.InnerText = "Địa chỉ:";
                    lbAddressInfo.InnerText = about.Address_VN;
                    lbPhone.InnerText = "Điện thoại:";
                }

                lbPhoneInfo.InnerText = about.Phone;
                lbFaxInfo.InnerText = about.Fax;
                lbEmailInfo.InnerText = about.Email;
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