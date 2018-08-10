using MvcFrontEnd.BusinessObjects;
using MvcFrontEnd.DataAccess;
using MvcFrontEnd.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace MvcFrontEnd.Controllers
{
    public class ServiceController : Controller
    {
        private Logger _log = LogManager.GetCurrentClassLogger();
        private const string __type = "Service";
        public ActionResult Index(int? id)
        {
#if DEBUG
            _log.Debug("id: {0}", id);
#endif
            _log.Debug("id: {0}", id);
            ViewBag.PageType = __type;
            AllCategoriesModel model = ServiceDAL.GetAllServices(id.HasValue ? id.Value : 0);
            if (id.HasValue && id.Value > 0)
            {
                model.Title = MenuDAL.GetTitle(id.Value);
            }

            return View(model);
        }

        public ActionResult Details(int id = 0)
        { 
#if DEBUG
            _log.Debug("id: {0}", id);
#endif
            ViewBag.PageType = __type;
            NewsBOL model = ServiceDAL.Get(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
