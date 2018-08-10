using BackEnd.DataAccess;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackEnd.Controllers
{
    public class SetupController : Controller
    {
        //
        // GET: /Setup/

        public ActionResult Index()
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }

        public ActionResult Header()
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

            return View(result.Data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Header(SiteConfig config)
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

        public ActionResult Footer()
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

            return View(result.Data);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Footer(SiteConfig config)
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
    }
}
