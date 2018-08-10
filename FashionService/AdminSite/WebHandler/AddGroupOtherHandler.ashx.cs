using AdminSite.BusinessObject;
using AdminSite.DataAccess;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AdminSite.WebHandler
{
    /// <summary>
    /// Summary description for AddGroupOtherHandler
    /// </summary>
    public class AddGroupOtherHandler : IHttpHandler
    {
        private Logger _log = LogManager.GetCurrentClassLogger();
        public void ProcessRequest(HttpContext context)
        {
            var jsonString = string.Empty;
            context.Request.InputStream.Position = 0;

            try
            {
                var inputStream = new StreamReader(context.Request.InputStream);
                jsonString = inputStream.ReadToEnd();
                inputStream.Close();
                //--
                OtherBOL other = JsonConvert.DeserializeObject<OtherBOL>(jsonString);
                other.InsertDate = DateTime.Now;
                other.UpdatedDate = DateTime.Now;
                other.IsGroup = true;

                var result = OtherDAL.InsertOrUpdate(other);
                //---
                object obj = new
                {
                    Code = result.Code,
                    Message = result.ErrorMessage,
                    Id = result.DbReturnValue,
                    Name = other.Name_VN
                };
                //---
                string responseString = JsonConvert.SerializeObject(obj);
                context.Response.ContentType = "application/json";
                context.Response.Write(responseString);
            }
            catch (Exception ex)
            {
                _log.Error(ex.ToString());
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