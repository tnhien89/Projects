using MvcFrontEnd.BusinessObjects;
using MvcFrontEnd.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFrontEnd.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Gallery/
        private const string __type = "ThuVienHinh";

        public ActionResult Index(int? id)
        {
            List<NewsBOL> lst = NewsDAL.GetAllWithType(__type, id.HasValue ? id.Value : 0);
            return View(lst);
        }

    }
}
