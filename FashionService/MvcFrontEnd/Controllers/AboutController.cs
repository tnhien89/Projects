using MvcFrontEnd.BusinessObjects;
using MvcFrontEnd.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFrontEnd.Controllers
{
    public class AboutController : Controller
    {
        //
        // GET: /About/

        private const string __type = "GioiThieu";

        public ActionResult Index(int id = 0)
        {
            NewsBOL news = AboutDAL.Get(id);

            return View(news);
        }

    }
}
