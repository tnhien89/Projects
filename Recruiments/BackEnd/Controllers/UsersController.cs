using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BackEnd.Controllers
{
    public class UsersController : Controller
    {
        //
        // GET: /Users/

        public ActionResult Index()
        {
            if (!Utilities.IsLoggedUser())
            {
                return RedirectToAction("Login", "Home");
            }

            return View();
        }
    }
}
