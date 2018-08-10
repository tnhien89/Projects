using Company.BusinessLogic;
using Company.Models;
using NLog;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Company.Admin.Controllers
{
    public class PostsController : Controller
    {
        private Logger _log = LogManager.GetCurrentClassLogger();

        // GET: Posts
        public ActionResult Index(string key)
        {
            PostService service = new PostService();
            if (string.IsNullOrEmpty(key))
            {
                ResultDTO<List<PostInfo>> rs = null;
                Task t1 = Task.Run(() => {
                    rs = service.Get((long)0, 0, "", "", "");
                });

                Task t2 = Task.Run(() => {
                    MenuService mService = new MenuService();
                    ViewData["Menu"] = mService.GetMenuFromXML(this.HttpContext); 
                });

                Task.WaitAll(new Task[] { t1, t2 });

                if (rs.Code < 0)
                {
                    _log.Error(rs.Message);
                    ModelState.AddModelError("Error", rs.Message);
                    return View("Index", new List<PostInfo>());
                }

                return View("Index", rs.Data);
            }
            else
            {
                ResultDTO<PostInfo> rs = null;
                Task t1 = Task.Run(() => {
                    rs = service.Get(key);
                });

                Task t2 = Task.Run(() => {
                    MenuService mService = new MenuService();
                    ViewData["Menu"] = mService.GetMenuFromXML(this.HttpContext); 
                });

                Task t3 = Task.Run(() => {
                    TownService tService = new TownService();
                    ViewData["TownItems"] = tService.Get(this.HttpContext);
                });

                Task[] tasks = new Task[] { t1, t2, t3 };
                Task.WaitAll(tasks);

                if (rs.Code < 0)
                {
                    _log.Error(rs.Message);
                    ModelState.AddModelError("Error", rs.Message);
                    return View("Index", new List<PostInfo>());
                }

                if (rs.Data == null)
                {
                    return RedirectToAction("Index", "Posts", new { key = "" });
                }

                return View("Details", rs.Data);
            }
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Index(PostInfo obj)
        {
            if (obj == null)
            {
                return View("Details", new PostInfo());
            }

            FileService fService = new FileService();
            fService.CheckAndSaveImage(obj, Request.Files);

            PostService service = new PostService();
            ResultDTO<PostInfo> rs = null;
            Task t1 = Task.Run(() => {
                rs = service.InserOrUpdate(obj);
            });

            Task t2 = Task.Run(() => {
                MenuService mService = new MenuService();
                ViewData["Menu"] = mService.GetMenuFromXML(this.HttpContext); ;
            });

            Task t3 = Task.Run(() => {
                TownService tService = new TownService();
                ViewData["TownItems"] = tService.Get(this.HttpContext);
            });

            Task[] tasks = new Task[] { t1, t2, t3 };
            Task.WaitAll(tasks);

            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
                ModelState.AddModelError("Error", rs.Message);
                return View("Details", obj);
            }

            return RedirectToAction("Index", "Posts", new { key = "" });
        }

        public ActionResult Add()
        {
            Task t1 = Task.Run(() => {
                MenuService mService = new MenuService();
                ViewData["Menu"] = mService.GetMenuFromXML(this.HttpContext); ;
            });

            Task t2 = Task.Run(() => {
                TownService tService = new TownService();
                ViewData["TownItems"] = tService.Get(this.HttpContext);
            });

            Task[] tasks = new Task[] { t1, t2 };
            Task.WaitAll(tasks);

            return View("Details", new PostInfo());
        }

        [HttpDelete]
        public JsonResult Delete(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new ResultDTO<List<DeleteError>>()
                {
                    Code = -1500
                });
            }

            PostService service = new PostService();
            ResultDTO<List<DeleteError>> rs = service.Delete(ids);
            if (rs.Code < 0)
            {
                _log.Error(rs.Message);
            }

            return Json(rs);
        }
    }
}