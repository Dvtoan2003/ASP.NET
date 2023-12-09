using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tranning.Models;

namespace Tranning.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            if(string.IsNullOrEmpty(HttpContext.Session.GetString("SessionUserName")))
            {
                return RedirectToAction(nameof(LoginController.Index), "Login");
            }
            // file index - default file(root file)
            // file mac dinh se chay o trong 1 controller
            return View();
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