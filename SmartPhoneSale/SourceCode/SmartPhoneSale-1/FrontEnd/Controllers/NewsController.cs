using FrontEnd.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult Index(int? id)
        {
            if (id == null || !id.HasValue || id.Value <= 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var result = NewsDAL.Get(id.Value);
            if (result.Code < 0)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(result.Data);
        }
    }
}
