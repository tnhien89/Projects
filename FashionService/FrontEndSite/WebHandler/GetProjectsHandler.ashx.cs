using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using FrontEndSite.BusinessObject;
using FrontEndSite.DataAccess;
using Newtonsoft.Json;

namespace FrontEndSite.WebHandler
{
    /// <summary>
    /// Summary description for GetProjectsHandler
    /// </summary>
    public class GetProjectsHandler : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            string tag = "[GetProjectsHandler][ProcessRequest]";
            LogHelpers.WriteStatus(tag, "Start.");

            string menuId = context.Request["MenuId"];
            string receiveTime = context.Request["ScanTime"];
            string receiveType = context.Request["ScanType"];

            object result;
            try
            {
                DateTime scanTime;

                if (receiveTime == "0")
                {
                    scanTime = DateTime.Now;
                }
                else
                {
                    scanTime = DateTime.ParseExact(receiveTime, "MMddyyyy HH:mm:ss.fff", CultureInfo.CurrentCulture);
                }

                var resultDAL = ProjectsDAL.GetPageItems(int.Parse(menuId), scanTime, receiveType);
                //---

                List<ProjectAjaxBOL> data = null;

                if (resultDAL.Code >= 0
                    && resultDAL.Data.Tables.Count > 0
                    && resultDAL.Data.Tables[0].Rows.Count > 0)
                {
                    data = new List<ProjectAjaxBOL>();
                    foreach (DataRow row in resultDAL.Data.Tables[0].Rows)
                    {
                        ProjectBOL project = new ProjectBOL(row);
                        ProjectAjaxBOL projectAjax = new ProjectAjaxBOL();
                        projectAjax.Id = project.Id;
                        //----
                        projectAjax.Name = "";
                        projectAjax.Name = Utilities.IsLangueEN() ? project.Name_EN : project.Name_VN;
                        projectAjax.MenuName = Utilities.IsLangueEN() ? project.MenuName_EN : project.MenuName_VN;
                        projectAjax.Name = string.Format("<a href='ProjectsInfo.aspx?Id={0}'>{1}</a>",
                            projectAjax.Id,
                            projectAjax.Name);
                        projectAjax.MenuName = string.Format("<a href='ProjectsInfo.aspx?Id={0}'>{1}</a>",
                            projectAjax.MenuId,
                            projectAjax.MenuName);
                        //----
                        projectAjax.ImageLink = Path.Combine(Utilities.GetDirectory("ImageProjectsDir"), project.ImageLink.Split('|')[0]);
                        projectAjax.ImageLink = string.Format("<img src='{0}' onclick=\"{1}\"/>",
                            projectAjax.ImageLink,
                            string.Format("javascript:window.location.href='ProjectsInfo.aspx?Id={0}'",
                                projectAjax.Id));
                        //---

                        data.Add(projectAjax);
                        //---
                        scanTime = project.UpdatedDate;
                    }

                }

                result = new { 
                    Code = resultDAL.Code,
                    Message = resultDAL.ErrorMessage,
                    Data = data, 
                    ScanTime = scanTime.ToString("MMddyyyy HH:mm:ss.fff")
                };
            }
            catch (Exception ex)
            {
                LogHelpers.WriteException(tag, ex.ToString());
                result = new { 
                    Code = ex.HResult,
                    Message = ex.Message
                };
            }

            string resJson = JsonConvert.SerializeObject(result);
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