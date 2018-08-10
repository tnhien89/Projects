using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FrontEndSite.BusinessObject;
using Newtonsoft.Json;

namespace FrontEndSite.UserControls
{
    public partial class ucContactForm : System.Web.UI.UserControl
    {
        private const string __tag = "[ucContactForm]";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Utilities.IsLangueEN())
            {
                lbContentHeader.InnerText = "Contact Form";
                lbName.InnerText = "Name";
                lbAddress.InnerText = "Address";
                lbPhone.InnerText = "Phone";
                lbSubject.InnerText = "Subject";
                lbContent.InnerText = "Content";
                btnSubmit.Text = "Send";
                btnClear.Text = "Clear";
            }
            else
            {
                lbContentHeader.InnerText = "Form Liên Hệ";
                lbName.InnerText = "Họ Tên";
                lbAddress.InnerText = "Địa chỉ";
                lbPhone.InnerText = "Điện thoại";
                lbSubject.InnerText = "Tiêu đề";
                lbContent.InnerText = "Nội dung";
                btnSubmit.Text = "Gửi";
                btnClear.Text = "Soạn lại";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string tag = __tag + "[btnSubmit_Click]";
            LogHelpers.WriteStatus(tag, "Start...");

            ContactBOL contact = StartCreateContact();

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Utilities.GetApplicationSettingsValue("AddContactWebHandlerUrl"));
                request.Method = "POST";
                request.ContentType = "application/json";

                string json = JsonConvert.SerializeObject(contact);
                byte[] requestBytes = Encoding.Default.GetBytes(json);
                //---
                Stream streamWriter = request.GetRequestStream();
                streamWriter.Write(requestBytes, 0, requestBytes.Length);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader streamRes = new StreamReader(response.GetResponseStream());
                string jsonResponse = streamRes.ReadToEnd();
                //---
                var result = JsonConvert.DeserializeObject<ResultBOL<int>>(jsonResponse);

                if (result.Code < 0)
                {
                    lbError.InnerText = result.ErrorMessage;
                    lbError.Visible = true;
                }
                else
                {
                    lbError.Visible = false;
                    StartClearFormContact();
                }
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //---
                lbError.InnerText = ex.Message;
                lbError.Visible = true;
            }
        }

        private void StartClearFormContact()
        {
            lbError.Visible = false;
            tbxName.Text = string.Empty;
            tbxEmail.Text = string.Empty;
            tbxAddress.Text = string.Empty;
            tbxPhone.Text = string.Empty;
            tbxSubject.Text = string.Empty;
            ckeContent.Text = string.Empty;
        }

        private ContactBOL StartCreateContact()
        {
            ContactBOL result = new ContactBOL() { 
                Name_VN = tbxName.Text,
                Address_VN = tbxAddress.Text,
                Phone = tbxPhone.Text,
                Email = tbxEmail.Text,
                Subject_VN = tbxSubject.Text,
                Content_VN = ckeContent.Text
            };

            if (Utilities.IsLangueEN())
            {
                result.Name_EN = tbxName.Text;
                result.Address_EN = tbxAddress.Text; 
                result.Subject_EN = tbxSubject.Text;
                result.Content_EN = ckeContent.Text;
            }

            return result;
        }
    }
}