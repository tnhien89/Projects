using BackEnd.DataAccess;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackEnd.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult Index(int? id)
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }

            if (id != null && !id.HasValue && id.Value > 0)
            {
                return RedirectToAction("Edit/" + id.Value.ToString());
            }

            var result = NewsDAL.GetAll(0);
            if (result.Code < 0)
            {
                throw new HttpException(result.ErrorMessage);
            }

            return View(result.Data);
        }

        public ActionResult Add()
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");

            }
            ViewData["ListMenu"] = MenuDAL.GetSelectList();
            return View("AddOrEdit", new News());
        }

        [HttpPost]
        public ActionResult Add(News news)
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }

            LogHelpers.LogHandler.Info("Start");
            ViewData["ListMenu"] = MenuDAL.GetSelectList();

            if (news == null)
            {
                return View("AddOrEdit", new News());
            }

            if (!ModelState.IsValid)
            {
                return View("AddOrEdit", news);
            }

            var result = NewsDAL.InsertOrUpdate(news);
            if (result.Code < 0)
            {
                return View("AddOrEdit", news);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int? id)
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }

            LogHelpers.LogHandler.Info("Start");
            ViewData["ListMenu"] = MenuDAL.GetSelectList();

            if (id == null || !id.HasValue || id.Value <= 0)
            {
                return RedirectToAction("Index");
            }

            var result = NewsDAL.Get(id.Value);
            if (result.Code < 0)
            {
                return RedirectToAction("Index");
            }

            return View("AddOrEdit", result.Data);
        }

        [HttpPost]
        public ActionResult Edit(News news)
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }

            LogHelpers.LogHandler.Info("Start");
            ViewData["ListMenu"] = MenuDAL.GetSelectList();
            if (news == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View("AddOrEdit", news);
            }

            var result = NewsDAL.InsertOrUpdate(news);
            if (result.Code < 0)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View("AddOrEdit", news);
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public JsonResult Delete(string listId) 
        {
            LogHelpers.LogHandler.Info("Start.");
            try
            {
                var result = NewsDAL.DeleteAll(listId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return Json(new ResultData<int>() { 
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
