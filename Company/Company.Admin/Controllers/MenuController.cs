using Company.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Company.BusinessLogic;
using NLog;
using System.Collections;

namespace Company.Admin.Controllers
{
    public class MenuController : Controller
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        // GET: Menu
        public ActionResult Index(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                ResultDTO<List<Menu>> rs = MenuService.Get(0, 0, "", "", "");
                if (rs.Code < 0)
                {
                    _log.Error(rs.Message);
                    ModelState.AddModelError("Error", rs.Message);
                    return View("Index", new List<Menu>());
                }

                return View("Index", rs.Data);
            }
            else
            {
                ResultDTO<Menu> rs = MenuService.Get(key);
                if (rs.Code < 0)
                {
                    _log.Error(rs.Message);
                    ModelState.AddModelError("Error", rs.Message);
                    return View("Index", new List<Menu>());
                }

                if (rs.Data == null)
                {
                    return RedirectToAction("Index", "Menu", new { key = "" });
                }

                return View("PostDetails", rs.Data);
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Index(Menu obj)
        {
            if (obj == null)
            {
                return View("PostDetails", new Menu());
            }

            ResultDTO<int> rs = MenuService.InserOrUpdate(obj);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
                ModelState.AddModelError("Error", rs.Message);
                return View("PostDetails", obj);
            }
            
            return RedirectToAction("Index", "Menu", new { key = "" });
        }

        public ActionResult Add()
        {
            return View("PostDetails", new Menu());
        }

        [HttpDelete]
        public JsonResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new ResultDTO<List<DeleteError>>()
                {
                    Code = -1500
                });
            }

            ResultDTO<List<DeleteError>> rs = MenuService.Delete(ids);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return Json(rs);
        }

        public ActionResult Design()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetDesignInfo()
        {
            MenuService service = new MenuService();
            ResultDTO<ArrayList> rs = service.GetDesignInfo();
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return Json(rs, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Design(string data, string menuIds)
        {
            if (string.IsNullOrEmpty(data))
            {
                return Json(new ResultDTO<int>() { Code = -1500 });
            }

            _log.Trace("data: {0}", data);
            Option obj = new Option("menu-data", data, menuIds);
            ResultDTO<int> rs = new OptionService().InsertOrUpdate(obj);

            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return Json(rs);
        }
    }
}