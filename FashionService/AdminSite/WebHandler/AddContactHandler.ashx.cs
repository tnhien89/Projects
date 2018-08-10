using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using AdminSite.BusinessObject;
using AdminSite.DataAccess;
using Newtonsoft.Json;

namespace AdminSite.WebHandler
{
    /// <summary>
    /// Summary description for AddContactHandler
    /// </summary>
    public class AddContactHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string tag = "[AddContactHandler][ProcessRequest]";
            LogHelpers.WriteStatus(tag, "Start...");
            //---
            ResultBOL<int> result;

            try
            {
                StreamReader reader = new StreamReader(context.Request.InputStream);
                string jsonRequest = reader.ReadToEnd();
                //---
                ContactBOL contact = JsonConvert.DeserializeObject<ContactBOL>(jsonRequest);
                contact.InsertDate = DateTime.Now;
                contact.UpdatedDate = DateTime.Now;
                //--
                result = ContactsDAL.InsertOrUpdate(contact);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                result = new ResultBOL<int>() { 
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                };
            }

            string jsonResponse = JsonConvert.SerializeObject(result);
            context.Response.ContentType = "application/json";
            context.Response.Write(jsonResponse);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}