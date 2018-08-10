using FrontEnd.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? menuId)
        {
            Session["SelectedMenuId"] = menuId;
            var result = NewsDAL.GetAll(menuId == null || !menuId.HasValue ? 0 : menuId.Value);
            if (result.Code < 0)
            {
                LogHelpers.LogHandler.Error(result.ErrorMessage);
                throw new HttpException(result.ErrorMessage);
            }

            return View(result.Data);
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
    }
}
