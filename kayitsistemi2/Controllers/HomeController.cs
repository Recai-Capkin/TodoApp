using kayitsistemi2.Areas.Identity.Data;
using kayitsistemi2.Data;
using kayitsistemi2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace kayitsistemi2.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly KayitdbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, KayitdbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        //private UserManager<IdentityUser> userManager { get; }

        //public HomeController(UserManager<IdentityUser> userManager)
        //{
        //    this.userManager = userManager;
        //}
        [HttpGet]
        public IActionResult Index()
        {
            // var users = userManager.GetUserId;
            var toplam_kullanici = _context.Users.Count();
            //var toplam_admin_sayisi = _context.UserRoles.Where(a => a.UserId == _context.Users.Select(c => c.Id));
            var toplam_gorev_sayisi = _context.TaskModels.Count();
            var toplam_yapilan_gorev_sayisi = _context.TaskModels.Where(g => g.TaskStatus == true).Count();
            var toplam_yapilmayan_gorev_sayisi = _context.TaskModels.Where(g => g.TaskStatus == false).Count();
            return View();
        }

        [HttpPost]
        public IActionResult Index(TaskModel model)
        {
            //persons.GroupBy(x => x.PersonId).Where(x => x.Count() > 1).Any(x => x)
           
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
