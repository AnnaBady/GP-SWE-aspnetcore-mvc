using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEBGPApps.Data;
using WEBGPApps.Models;

namespace WEBGPApps.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly RoleManager<IdentityRole> _roleManager;
            ApplicationDbContext db;

            public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager , ApplicationDbContext context)
            {
                _roleManager = roleManager;
                _userManager = userManager;
                db = context;

            }
        [HttpGet]
        public IActionResult AddUsers()
        {
            return View();
        }
        public IActionResult Delete()
        {
            var sss=db.News.ToList();
            return View(sss);
        }
        public async Task<IActionResult> GStudentList()
        {
            return View();
        }
        private async Task<List<string>> GetUserRoles(ApplicationUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }

        public IActionResult News() 
        {
            return View();
        }

        public IActionResult ManageCourses()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Save_News(News model)
        {
            db.News.Add(model);
            db.SaveChanges();
            return RedirectToAction("News");
        }

        public IActionResult Save_ManageCourses(Courses model1)
        {
            db.Courses.Add(model1);
            db.SaveChanges();
            return RedirectToAction("ManageCourses");
        }

    }
}