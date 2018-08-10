using MvcFrontEnd.BusinessObjects;
using MvcFrontEnd.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFrontEnd.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/

        public ActionResult Index(int? id)
        {
            List<NewsBOL> lst = NewsDAL.GetAll(id.HasValue ? id.Value : 0);
            return View(lst);
        }

        public ActionResult Details(int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return RedirectToAction("Index");
            }

            NewsBOL news = NewsDAL.Get(id.Value);
            return View(news);
        }
    }
}
