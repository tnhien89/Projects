using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AdminSite.BusinessObject;
using AdminSite.DataAccess;

namespace AdminSite.UserControls
{
    public partial class ucAbout : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void StartLoadAboutDetail()
        {
            string tag = "[ucAbout][StartLoadAboutDetail]";

            var result = AboutsDAL.Get();
            if (result.Code < 0)
            {
                lbError.InnerText = result.ErrorMessage;
                lbError.Visible = true;
                return;
            }

            lbError.Visible = false;
            try
            {
                AboutBOL about = new AboutBOL(result.Data.Tables[0].Rows[0]);
                hfId.Value = about.Id.ToString();
                tbxNameVN.Text = about.Name_VN;
                tbxNameEN.Text = about.Name_EN;
                tbxAddress.Text = about.Address_VN;
                tbxAddressEN.Text = about.Address_EN;
                tbxFax.Text = about.Fax;
                tbxPhone.Text = about.Phone;
                tbxEmail.Text = about.Email;
                tbxGoogleCode.Text = about.GoogleMaps;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //---
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            AboutBOL obj = CreateAboutObject();
            //--
            var result = AboutsDAL.InsertOrUpdate(obj);
            if (result.Code < 0)
            {
                lbError.InnerText = result.ErrorMessage;
                lbError.Visible = true;

                return;
            }

            lbError.Visible = false;
        }

        private AboutBOL CreateAboutObject()
        {
            string tag = "[ucAbout][CreateAboutObject]";

            try
            {
                AboutBOL about = new AboutBOL() { 
                    Id = string.IsNullOrEmpty(hfId.Value) ? 0 : int.Parse(hfId.Value),
                    Name_VN = tbxNameVN.Text,
                    Name_EN = tbxNameEN.Text,
                    Address_VN = tbxAddress.Text,
                    Address_EN = tbxAddressEN.Text,
                    Phone = tbxPhone.Text,
                    Fax = tbxFax.Text,
                    Email = tbxEmail.Text,
                    GoogleMaps = tbxGoogleCode.Text
                };

                //--
                return about;
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                lbError.InnerText = ex.Message;
                lbError.Visible = true;

                return null;
            }
        }
    }
}