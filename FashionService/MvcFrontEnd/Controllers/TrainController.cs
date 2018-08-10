using MvcFrontEnd.BusinessObjects;
using MvcFrontEnd.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFrontEnd.BusinessObjects;
using MvcFrontEnd.DataAccess;
using MvcFrontEnd.Models;
using NLog;

namespace MvcFrontEnd.Controllers
{
    public class TrainController : Controller
    {
        //
        // GET: /Train/
        private Logger _log = LogManager.GetCurrentClassLogger();
        private const string __type = "DaoTao";
        public ActionResult Index(int? id)
        {
            ViewBag.PageType = __type;
            AllCategoriesModel model = CategoryDAL.GetAllWithType(id.HasValue ? id.Value : 0, true);
            return View(model);
        }

        public ActionResult Details(int id = 0)
        {
#if DEBUG
            _log.Debug("id: {0}", id);
#endif
            ViewBag.PageType = __type;
            NewsBOL model = NewsDAL.GetWithType(__type, id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
