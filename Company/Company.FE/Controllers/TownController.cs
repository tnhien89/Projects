using Company.BusinessLogic;
using Company.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Company.FE.Controllers
{
    public class TownController : Controller
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        // GET: Town
        public ActionResult Index(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return PostsForTown(key);
            }

            Task t1 = Task.Run(() => {
                ViewData["TownItems"] = new TownService().Get(this.HttpContext);
            });

            Task t2 = Task.Run(() => {
                ViewData["CountItems"] = new TownService().GetPostsCount();
            });

            Task.WaitAll(new Task[] { t1, t2 });

            return View();
        }

        private ActionResult PostsForTown(string key)
        {
            ResultDTO<List<PostInfo>> rs = null;

            Task t1 = Task.Run(() => {
                TownService service = new TownService();
                rs = service.GetPosts(key);
            });

            Task t2 = Task.Run(() => {
                TownService tService = new TownService();
                ViewData["TownItems"] = tService.Get(this.HttpContext);
            });

            Task.WaitAll(new Task[] { t1, t2 });
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            if (rs.Data == null)
            {
                rs.Data = new List<PostInfo>();
            }

            return View("~/Views/Recruitment/Index.cshtml", rs.Data);
        }
    }
}