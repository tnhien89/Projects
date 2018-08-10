using MvcFrontEnd.BusinessObjects;
using MvcFrontEnd.DataAccess;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private Logger _log = LogManager.GetCurrentClassLogger();
        public ActionResult Index()
        {
            ViewBag.Title = "Dịch Vụ";
            List<NewsBOL> lst = ServiceDAL.GetTop(12);
            return View(lst);
        }

        public ActionResult Contact()
        {
            ContactBOL contact = ContactsDAL.GetInfo();
            return View(contact);
        }

        [HttpPost]
        public ActionResult Contact(ContactBOL model)
        {
#if DEBUG
            _log.Debug("================ Start ====================");
#endif
            if (model == null)
            {
                return View(new ContactBOL());
            }

            ResultData<int> result = ContactsDAL.InsertOrUpdate(model);
            if (result.Code < 0)
            {
                ModelState.AddModelError("Error", result.Message);
                return View(model);
            }

            return View(new ContactBOL());
        }
    }
}
