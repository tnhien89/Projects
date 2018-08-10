using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace AdminSite.WebHandler
{
    /// <summary>
    /// Summary description for UploadMultipleFiles
    /// </summary>
    public class UploadMultipleFiles : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string tag = "[UploadMultipleFiles][ProcessRequest]";

            //string receivePaths = context.Request["Paths"];
            object result;

            try
            {
                string dirKey = "ImageProjectsDir";
                //---
                string dir = Utilities.GetDirectory(dirKey);
                string folder = DateTime.Now.ToString("MMddyyyy");
                dir = Path.Combine(dir, folder);
                //----
                string fullDir = context.Server.MapPath("~/" + dir);
                if (!Directory.Exists(fullDir))
                {
                    Directory.CreateDirectory(fullDir);
                }

                string listImages = string.Empty;

                listImages = StartUploadFile(context.Request.Files, folder, fullDir);
                //----
                result = new { 
                    Code = 0,
                    Data = listImages
                };
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                //---
                result = new { 
                    Code = ex.HResult,
                    Message = ex.Message
                };
            }

            string resJson = JsonConvert.SerializeObject(result);
            context.Response.ContentType = "application/json";
            context.Response.Write(resJson);
        }

        private string StartUploadFile(HttpFileCollection httpFileCollection, string folder, string fullDir)
        {
            string tag = "[UploadMultipleFiles][StartUploadFile]";
            LogHelpers.WriteStatus(tag, "Folder: " + folder, "Start...");
            //=---
            if (httpFileCollection == null)
            {
                return string.Empty;
            }
            //----
            string result = string.Empty;

            foreach (string fName in httpFileCollection)
            {
                try
                {
                    HttpPostedFile file = httpFileCollection[fName];
                    //---
                    string fileName = Path.GetFileName(file.FileName);

                    string dir = Path.Combine(fullDir, folder);
                    
                    //---
                    file.SaveAs(Path.Combine(fullDir, fileName));
                    //---
                    fileName = Path.Combine(folder, fileName);
                    if (string.IsNullOrEmpty(result))
                    {
                        result = fileName;
                    }
                    else
                    {
                        result += "|" + fileName;
                    }
                }
                catch (Exception ex)
                {
                    LogHelpers.WriteException(tag, ex.ToString());
                }
            }

            return result;
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