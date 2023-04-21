using Forum.Data;
using Forum.Models;
using Forum.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Forum.Controllers
{
  
    public class UserController : Controller
    {
        
        private readonly MySqlContext _context;
        private readonly IWebHostEnvironment _env;
        public UserController(MySqlContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Registrasi()
        {
            
            return View();
        }
        [HttpPost]
        
        public IActionResult Registrasi([FromForm] UserForm data, IFormFile photo ,int idRole = 2)
        {
            if (photo.Length > 10000000)
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

            return RedirectToAction("Login", "Account");
        }

        
        public IActionResult UserLog()
        {

            ViewBag.idUser = (int)TempData["idUser"];
            ViewBag.Statuss = _context.Statuss.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            });

            return View();
        }
        [HttpPost]
        public IActionResult UserLog([FromForm] Post data)
        {
            var stat = _context.Statuss.FirstOrDefault(x => x.Id == data.Status.Id);

            var post = new Post()
            {
                Title = data.Title,
                Content = data.Content,
                Time = data.Time,
                User = _context.Users.Where(u => u.Id == data.User.Id).FirstOrDefault(),
                Status = stat
            };

            _context.Posts.Add(post);
            _context.SaveChanges();

            return View();
        }
    }
}
