using BackEnd.DataAccess;
using BackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackEnd.Controllers
{
    public class MenuController : Controller
    {
        //
        // GET: /Menu/

        public ActionResult Index(int ? id)
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }

            if (id != null && id.HasValue && id.Value > 0)
            {
                return RedirectToAction("Edit", new { id = id });
            }

            LogHelpers.LogHandler.Info("Start");
            var result = MenuDAL.GetAll();
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

            return View("AddOrEdit", new Menu());
        }

        [HttpPost]
        public ActionResult Add(Menu menu)
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");

            }
            LogHelpers.LogHandler.Info("Start");
            try
            {
                if (menu == null)
                {
                    return View(new Menu());
                }

                if (!ModelState.IsValid)
                {
                    return View("AddOrEdit", menu);
                }

                var result = MenuDAL.InsertOrUpdate(menu);
                if (result.Code < 0)
                {
                    LogHelpers.LogHandler.Error(result.ErrorMessage);
                    ModelState.AddModelError("", result.ErrorMessage);
                    return View("AddOrEdit", menu);
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                ModelState.AddModelError("", ex.Message);

                return View("AddOrEdit", menu);
            }
        }

        public ActionResult Edit(int? id)
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }

            if (id == null || !id.HasValue)
            {
                return RedirectToAction("Index");
            }

            var result = MenuDAL.Get(id.Value);
            if (result.Code < 0)
            {
                LogHelpers.LogHandler.Error(result.ErrorMessage);
                return RedirectToAction("Index");
            }

            return View("AddOrEdit", result.Data);
        }

        [HttpPost]
        public ActionResult Edit(Menu menu)
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }

            LogHelpers.LogHandler.Info("Start");
            if (menu == null)
            {
                return View("AddOrEdit", new Menu());
            }

            if (!ModelState.IsValid)
            {
                return View("AddOrEdit", menu);
            }

            var result = MenuDAL.InsertOrUpdate(menu);
            if (result.Code < 0)
            {
                LogHelpers.LogHandler.Error(result.ErrorMessage);
                ModelState.AddModelError("", result.ErrorMessage);
                return View("AddOrEdit", menu);
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public JsonResult Delete(string listId)
        {
            LogHelpers.LogHandler.Info("Start.");
            try
            {
                var result = MenuDAL.DeleteAll(listId);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogHelpers.LogHandler.Error(ex.ToString());
                return Json(new ResultData<int>()
                {
                    Code = ex.HResult,
                    ErrorMessage = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
