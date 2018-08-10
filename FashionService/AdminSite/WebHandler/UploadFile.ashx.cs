using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdminSite.WebHandler
{
    /// <summary>
    /// Summary description for UploadFile
    /// </summary>
    public class UploadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpPostedFile files = context.Request.Files["upload"];
            string CKEditorFuncNum = context.Request["CKEditorFuncNum"];

            string fileName = System.IO.Path.GetFileName(files.FileName);
            files.SaveAs(context.Server.MapPath("~\\") + string.Format("\\Upload\\Images\\{0}", fileName));

            string url = string.Format("/Upload/Images/{0}", fileName);

            context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\");</script>");
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