using FastDeploy.Admin.Models;
using FastDeploy.Models;
using FastDeploy.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics;

namespace FastDeploy.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly Logger _logger;
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("login")]
        public IActionResult Login()
        {
            return View(new UserLoginModel());
        }

        [HttpPost]
        [ActionName("login")]
        public IActionResult Login(UserLoginModel loginModel)
        {
            return View(loginModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}