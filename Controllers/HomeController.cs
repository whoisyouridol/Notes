using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Notes.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Notes.Areas.Identity.Data;

namespace Notes.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public HomeController(UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager)
        {
            this._roleManager = _roleManager;
            this._userManager = _userManager;
        }
        public ActionResult Description()
        {
            return View();
        }
        public async Task<IActionResult> Index()
        {
            //in my task I had to add first user in DB when I launch this project firstly and here is my solution
            if (!_userManager.Users.Any())
            {
                await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                const string firstUserLogin = "firstUserinDb@gmail.com";
                var firstUserPassword = "}Lk@-wz2G$";
                var firstUser = new ApplicationUser()
                {
                    UserName = firstUserLogin,
                    Email = firstUserLogin
                };
                await _userManager.CreateAsync(firstUser, firstUserPassword);
                await _userManager.AddToRoleAsync(firstUser, "Admin");
            }
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Users()
        {
            return View(_userManager.Users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
