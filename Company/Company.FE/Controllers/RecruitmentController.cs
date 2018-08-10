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
    public class RecruitmentController : Controller
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        // GET: Recruitment
        public ActionResult Index(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return this.Details(key);
            }

            ResultDTO<List<PostInfo>> rs = null;

            Task t1 = Task.Run(() => {
                PostService service = new PostService();
                rs = service.Get((long)0, 0, "Type='tuyen-dung'", "", "");
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

            return View(rs.Data);
        }

        private ActionResult Details(string key)
        {
            ResultDTO<PostInfo> rs = null;

            Task t1 = Task.Run(() => {
                PostService service = new PostService();
                rs = service.Get(key);
            });

            Task t2 = Task.Run(() => {
                TownService tService = new TownService();
                ViewData["TownItems"] = tService.Get(this.HttpContext);
            });

            Task.WaitAll(new Task[] { t1, t2 });
            
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
                return View("Index");
            }

            if (rs.Data == null)
            {
                rs.Data = new PostInfo();
            }

            return View("Details", rs.Data);
        }
    }
}