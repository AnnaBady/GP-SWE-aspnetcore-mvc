using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEBGPApps.Data;
using WEBGPApps.Models;

namespace WEBGPApps.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            var Neww = db.News.ToList();
            return View(Neww);
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}