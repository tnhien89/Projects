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
    /// Summary description for SortOtherTypeHandler
    /// </summary>
    public class SortOtherTypeHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string tag = "[SortOtherTypeHandler][ProcessRequest]";
            LogHelpers.WriteStatus(tag, "Start...");

            context.Request.InputStream.Position = 0;
            object result;

            try
            {
                var inputStream = new StreamReader(context.Request.InputStream);
                string receiveJson = inputStream.ReadToEnd();

                OtherTypeUpdateIndexBOL[] receiveData = JsonConvert.DeserializeObject<OtherTypeUpdateIndexBOL[]>(receiveJson);
                var updateResult = OtherTypeDAL.UpdateIndex(receiveData);

                result = new
                {
                    Code = updateResult.Code,
                    Message = updateResult.ErrorMessage
                };
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //--
                result = new
                {
                    Code = ex.HResult,
                    Message = ex.Message
                };
            }
            finally
            {
                LogHelpers.WriteStatus(tag, "End.");
            }

            string responseString = JsonConvert.SerializeObject(result);
            context.Response.ContentType = "application/json";
            context.Response.Write(responseString);
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