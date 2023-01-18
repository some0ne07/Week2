using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication2.Models;
using MongoDB.Bson;
using System.Dynamic;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserService _userService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        public async Task addocs(User user)
        {
            BsonDocument doc = new BsonDocument
            {
                { "name", user.name},
                {"count", user.count}
            };

            await _userService.Addocs(doc);
        }

        [HttpPost]
        public async Task<ActionResult> Index(User user)
        {
            await addocs(user);

            return View();
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Privacy()
        {
            var dbList = await _userService.Get();
            BsonDocument t = new BsonDocument();
            foreach (var item in dbList)
            {
                t = item;

            }
            ViewData["name"] = t["name"];
            ViewData["count"] = t["count"];
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
