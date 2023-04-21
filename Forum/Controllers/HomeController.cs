using Forum.Data;
using Forum.Models;
using Forum.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Forum.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MySqlContext _context;

        public HomeController(ILogger<HomeController> logger, MySqlContext c)
        {
            _logger = logger;
            _context = c;
        }

        public IActionResult Index()
        {
            /*var post1 = new Post();
            var respon1 = new Respon();

            var viewModel = new Tamp
            {
                Post = post1;
                Respon = respon1;
            }*/

            var home = _context.Posts.Include(x => x.User).ToList();
            //ViewBag.idPost = (int)TempData["idPost"];

            return View(home);
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