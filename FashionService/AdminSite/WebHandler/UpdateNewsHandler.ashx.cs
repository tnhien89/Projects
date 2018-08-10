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
    /// Summary description for UpdateNewsHandler
    /// </summary>
    public class UpdateNewsHandler : IHttpHandler
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        public void ProcessRequest(HttpContext context)
        {
            var inputStream = new StreamReader(context.Request.InputStream);
            string receiveJson = inputStream.ReadToEnd();

            try
            {
                dynamic news = JsonConvert.DeserializeObject(receiveJson);
                object obj = null;

                if (news.Priority != null)
                {
                    obj = new
                    {
                        Id = Convert.ToInt32(news.Id),
                        Priority = Convert.ToInt32(news.Priority)
                    };
                }
                else
                {
                    obj = new {
                        Id = Convert.ToInt32(news.Id),
                        Disable = Convert.ToBoolean(news.Disable)
                    };
                }

                //NewsBOL news = JsonConvert.DeserializeObject<NewsBOL>(receiveJson);
                var result = NewsDAL.UpdatePriorityOrDisable(obj);
                if (result.Code < 0)
                {
                    _log.Error(result.ErrorMessage);
                }

                string responseString = JsonConvert.SerializeObject(result);
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