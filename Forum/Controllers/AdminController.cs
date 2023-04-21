using Forum.Data;
using Forum.Models;
using Forum.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers
{
    public class AdminController : Controller
    {
        private readonly MySqlContext _context;
        private readonly IWebHostEnvironment _env;
        public AdminController(MySqlContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            var home = _context.Posts.Include(x => x.User).ToList();
            
            return View(home);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]

        public IActionResult Create([FromForm] UserForm data, IFormFile photo, int idRole = 1)
        {
            if (photo.Length > 1000000)
            {
                ModelState.AddModelError(nameof(data.Photo), "Ukuran foto melebihi batas");
            }


            var filename = "photo_" + data.Username + Path.GetExtension(photo.FileName);
            var filepath = Path.Combine(_env.WebRootPath, "upload", filename);

            using (var stream = System.IO.File.Create(filepath))
            {
                photo.CopyTo(stream);
            }

            var user = new User()
            {
                Username = data.Username,
                Password = data.Password,
                Fullname = data.Fullname,
                Email = data.Email,
                Photo = filename,
                Role = _context.Roles.Where(r => r.Id == idRole).FirstOrDefault()
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "Admin");
        }
    }
}
