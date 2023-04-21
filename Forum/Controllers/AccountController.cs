using Forum.Data;
using Forum.Models;
using Forum.Models.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Bcpg;
using System.Security.Claims;

namespace Forum.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly MySqlContext _context;

        public AccountController(MySqlContext context)
        {
            _context = context;
        }

   

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromForm] Login data)
        {
            if (data.Username == "admin" && data.Password =="123")
            {
                return RedirectToAction("Index", "Admin");
                
            }
            var cariUser = _context.Users.Where(x =>
            x.Username == data.Username &&
            x.Password == data.Password).FirstOrDefault();

            TempData["idUser"] = cariUser.Id;

            if (cariUser !=null)
            {
                return RedirectToAction("UserLog","User");
            }
            
            return View("UserLog");
        }

    }

}
