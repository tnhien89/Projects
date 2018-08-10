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
    public class NewsController : Controller
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        // GET: News
        public ActionResult Index(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return Details(key);
            }

            ResultDTO<List<PostInfo>> rs = null;
            Task t1 = Task.Run(() => {
                PostService service = new PostService();
                rs = service.Get((long)0, 0, "Type='tin-tuc'", "", "");
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
            }


            if (rs.Data == null)
            {
                rs.Data = new List<PostInfo>();
            }

            return View(rs.Data);
        }

        private ActionResult Details(string key)
        {
            PostService service = new PostService();
            ResultDTO<PostInfo> rs = service.Get(key);

            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            if (rs.Data == null)
            {
                rs.Data = new PostInfo();
            }

            return View("Details", rs.Data);
        }
    }
}