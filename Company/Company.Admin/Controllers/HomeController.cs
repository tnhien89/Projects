using Company.BusinessLogic;
using Company.Extensions;
using Company.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Company.Admin.Controllers
{
    public class HomeController : Controller
    {
        private Logger _log = LogManager.GetCurrentClassLogger();
        // GET: Home
        public ActionResult Index(string key)
        {
            return View();
        }

        public ActionResult Login()
        {
            return View("Login", new UserInfo());
        }

        [HttpPost]
        public ActionResult Login(UserInfo obj)
        {
            if (obj == null)
            {
                return View("Login", new UserInfo());
            }

            UserService service = new UserService();
            ResultDTO<UserInfo> rs = service.Login(obj.Username, obj.Password.MD5Encryption());

            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
                ModelState.AddModelError("Error", rs.Message);
                return View("Login", obj);
            }

            Session["LoggedInfo"] = rs.Data;

            return RedirectToAction("Index", "Home", new { key = "" });
        }

        [HttpPost]
        public JsonResult Logout()
        {
            Session["LoggedInfo"] = null;
            return Json(new ResultDTO<int>()
            {
                Code = 0
            });
        }

        [HttpPost]
        public void CKUploadFiles()
        {
            HttpPostedFileBase data = Request.Files["upload"];
            if (data == null || string.IsNullOrEmpty(data.FileName))
            {
                Response.Write("<script>alert('Object reference not set to an instance of an object.');</script>");
            }
            else
            {
                FileService service = new FileService();
                ResultDTO<UploadFileInfo> rs = service.Insert(data);
                if (rs.Code < 0)
                {
                    _log.Error(rs.Message);
                    Response.Write(string.Format("<script>alert('{0}');</script>", rs.Message));
                }
                else
                {
                    Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" +
                                            Request["CKEditorFuncNum"] +
                                            ", \"/Files/" + rs.Data.FileName + "\");</script>");
                }
            }

            Response.End();
        }

        public ActionResult Files(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return new EmptyResult();
            }

            _log.Trace("key: {0}", key);

            FileService service = new FileService();
            ResultDTO<UploadFileInfo> rs = service.Get(key);

            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
                return new EmptyResult();
            }

            if (rs.Data == null || string.IsNullOrEmpty(rs.Data.FileData))
            {
                return new EmptyResult();
            }

            return File(Convert.FromBase64String(rs.Data.FileData), "image/jpeg");
        }
    }
}