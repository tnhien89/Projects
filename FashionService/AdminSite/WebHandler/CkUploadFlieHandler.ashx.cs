using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace AdminSite.WebHandler
{
    /// <summary>
    /// Summary description for CkUploadFlieHandler
    /// </summary>
    public class CkUploadFlieHandler : IHttpHandler
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        public void ProcessRequest(HttpContext context)
        {
            HttpPostedFile uploads = context.Request.Files["upload"];
            string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
            string file = DateTime.Now.Ticks.ToString() + System.IO.Path.GetExtension(uploads.FileName);

            string dir = ConfigurationManager.AppSettings["ImagesDir"];
            string folder = DateTime.Now.ToString("MMddyyyy");
            dir = Path.Combine(dir, folder);
            string serverPath = Path.Combine(context.Server.MapPath("~/" + dir));

            try
            {
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }

                uploads.SaveAs(Path.Combine(serverPath, file));
                //provide direct URL here
                string url = Path.Combine(ConfigurationManager.AppSettings["ImagesUrl"], folder, file).Replace("\\", "/");

                context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" +
                                            CKEditorFuncNum +
                                            ", \"" + url + "\");</script>");
                context.Response.End();
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