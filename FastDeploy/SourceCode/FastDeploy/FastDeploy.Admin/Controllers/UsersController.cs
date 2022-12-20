using Microsoft.AspNetCore.Mvc;

namespace FastDeploy.Admin.Controllers
{
    public class UsersController : Controller
    {
        [Route("Users")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
