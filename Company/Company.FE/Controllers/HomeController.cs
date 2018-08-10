using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Company.Models;
using Company.BusinessLogic;
using NLog;
using System.Collections;
using System.Threading.Tasks;

namespace Company.FE.Controllers
{
    public class HomeController : Controller
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        // GET: Home
        public ActionResult Index()
        {
            ResultDTO<ArrayList> rs = null;
            Task t1 = Task.Run(() => {
                PostService service = new PostService();
                rs = service.GetTopHotAndNew(10, 10);
            });

            Task t2 = Task.Run(() => {
                ViewData["TownItems"] = new TownService().Get(this.HttpContext);
            });

            Task t3 = Task.Run(() => {
                ViewData["CountItems"] = new TownService().GetPostsCount();
            });

            Task.WaitAll(new Task[] { t1, t2, t3 });

            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
                return View(new ArrayList());
            }

            return View(rs.Data);
        }

        public ActionResult Files(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return new EmptyResult();
            }

            _log.Trace("key: {0}", key);

            FileService service = new FileService();
            ResultDTO<UploadFileInfo> rs = service.Get(key);

            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
                return new EmptyResult();
            }

            if (rs.Data == null || string.IsNullOrEmpty(rs.Data.FileData))
            {
                return new EmptyResult();
            }

            return File(Convert.FromBase64String(rs.Data.FileData), "image/jpeg");
        }
    }
}