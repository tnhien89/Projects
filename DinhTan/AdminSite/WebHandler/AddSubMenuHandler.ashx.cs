using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using AdminSite.BusinessObject;
using AdminSite.DataAccess;
using Newtonsoft.Json;

namespace AdminSite.WebHandler
{
    /// <summary>
    /// Summary description for AddSubMenuHandler
    /// </summary>
    public class AddSubMenuHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string tag = "[AddSubMenuHandler][ProcessRequest]";

            var jsonString = string.Empty;
            context.Request.InputStream.Position = 0;

            try
            {
                var inputStream = new StreamReader(context.Request.InputStream);
                jsonString = inputStream.ReadToEnd();
                inputStream.Close();
                //--
                MenuBOL menu = JsonConvert.DeserializeObject<MenuBOL>(jsonString);
                menu.InsertDate = DateTime.Now;
                menu.UpdatedDate = DateTime.Now;

                var result = MenuDAL.InsertOrUpdate(menu);
                //---
                object obj = new { 
                    Code = result.Code,
                    Message = result.ErrorMessage,
                    Id = result.DbReturnValue,
                    Name = menu.Name_VN
                };
                //---
                string responseString = JsonConvert.SerializeObject(obj);
                context.Response.ContentType = "application/json";
                context.Response.Write(responseString);
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
            }
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