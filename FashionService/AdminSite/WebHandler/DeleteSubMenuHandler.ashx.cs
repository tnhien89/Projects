using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AdminSite.DataAccess;
using Newtonsoft.Json;

namespace AdminSite.WebHandler
{
    /// <summary>
    /// Summary description for DeleteSubMenuHandler
    /// </summary>
    public class DeleteSubMenuHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            object resObj;

            try
            {
                var result = MenuDAL.DeleteAll(context.Request["Id"]);
                resObj = new { 
                    Code = result.Code,
                    Message = result.ErrorMessage
                };
            }
            catch (Exception ex)
            {
                resObj = new
                { 
                    Code = ex.HResult,
                    Message = ex.Message
                };
            }

            string resJson = JsonConvert.SerializeObject(resObj);
            context.Response.ContentType = "application/json";
            context.Response.Write(resJson);
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