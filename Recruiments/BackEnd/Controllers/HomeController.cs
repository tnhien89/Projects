using BackEnd.DataAccess;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackEnd.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }

            return RedirectToAction("UserProfile", "Home");
            //var result = SiteSettingsDAL.GetSiteConfig();
            //if (result.Code < 0)
            //{
            //    LogHelpers.LogHandler.Error(result.ErrorMessage);
            //}

            //return View(result.Data);
        }

        [HttpPost]
        public ActionResult Index(SiteConfig config)
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }

            LogHelpers.LogHandler.Info("Start.");
            if (config == null)
            {
                return View(new SiteConfig());
            }

            var result = SiteSettingsDAL.SaveSiteConfig(config);
            if (result.Code < 0)
            {
                LogHelpers.LogHandler.Error(result.ErrorMessage);
                ModelState.AddModelError("", result.ErrorMessage);
            }

            return View(config);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginInfo info)
        {
            try
            {
                LogHelpers.LogHandler.Info("Username: " + info.Username);
                var result = UsersDAL.Login(info.Username, info.Password);
                if (result.Code < 0)
                {
                    ModelState.AddModelError("", result.ErrorMessage);
                    return View(info);
                }

                return RedirectToAction("UserProfile"); 
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return View(new LoginInfo());
            }
        }

        public ActionResult UserProfile()
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }
            LogHelpers.LogHandler.Info("Start");
            //---
            ViewData["ListCities"] = new List<SelectListItem>();
            UserProfile user = Utilities.GetLoggedUser();
            if (user == null)
            {
                return View(new UserProfile());
            }

            return View(user);
        }

        [HttpPost]
        public ActionResult UserProfile(UserProfile user)
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }
            LogHelpers.LogHandler.Info("Start.");
            try
            {
                ViewData["ListCities"] = new List<SelectListItem>();
                if (user == null)
                {
                    return RedirectToAction("UserProfile");
                }

                var result = UsersDAL.Update(user);
                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                    ModelState.AddModelError("", result.ErrorMessage);
                }

                Session["LoggedUser"] = user;

                return View(user);
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return RedirectToAction("UserProfile");
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public JsonResult UploadLogo(HttpPostedFileBase file, string fileName)
        {
            try
            {
                LogHelpers.LogHandler.Info("fileName: " + file.FileName);
                var result = Utilities.SaveFileToServer(file, "Logo", fileName);

                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return Json(new ResultData<string>() { 
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Setup()
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }

            var result = SiteSettingsDAL.GetSiteConfig();
            if (result.Code < 0)
            {
                LogHelpers.LogHandler.Error(result.ErrorMessage);
            }

            return View("Setup", result.Data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Setup(SiteConfig config)
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }

            LogHelpers.LogHandler.Info("Start.");
            if (config == null)
            {
                return View("Setup", new SiteConfig());
            }

            var result = SiteSettingsDAL.SaveSiteConfig(config);
            if (result.Code < 0)
            {
                LogHelpers.LogHandler.Error(result.ErrorMessage);
                ModelState.AddModelError("", result.ErrorMessage);
            }

            return View("Setup", config);
        }
    }
}
