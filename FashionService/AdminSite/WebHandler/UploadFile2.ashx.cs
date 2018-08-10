using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace AdminSite.WebHandler
{
    /// <summary>
    /// Summary description for UploadFile2
    /// </summary>
    public class UploadFile2 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            HttpFileCollection files = context.Request.Files;
            if (files != null && files.Count > 0)
            {
                HttpPostedFile file = files[0];
                string dir = ConfigurationManager.AppSettings["ImagesDir"];
                string folder = DateTime.Now.ToString("MMddyyyy");
                dir = Path.Combine(dir, folder);
                string serverPath = Path.Combine(context.Server.MapPath("~/" + dir));
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }

                string filePath = StartUploadFile(file, folder, serverPath);
                string resJson = JsonConvert.SerializeObject(new { 
                    Code = 0,
                    Data = filePath
                });

                context.Response.ContentType = "application/json";
                context.Response.Write(resJson);
            }
        }

        private string StartUploadFile(HttpPostedFile file, string folder, string fullDir)
        {
#if DEBUG
            LogHelpers.Log.Debug("folder: {0} - fullDir: {1}", folder, fullDir);
#endif
            string fileName = Path.GetFileName(file.FileName);
            string dir = Path.Combine(fullDir, folder);

            //---
            try
            {
                file.SaveAs(Path.Combine(fullDir, fileName));
                //---
                fileName = Path.Combine(folder, fileName);

                return fileName;
            }
            catch (Exception ex)
            {
                LogHelpers.Log.Error(ex.ToString());
            }

            return "";
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