using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kayitsistemi2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {

        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(roles);
        }
        public IActionResult AddRole() => View();

        [HttpPost]
        public async Task<IActionResult> AddRole(IdentityRole identityRole)
        {
            if (identityRole.Name != null)
            {
                await _roleManager.CreateAsync(new IdentityRole { Name=identityRole.Name });
            }
            return RedirectToAction("Index");
        }
    }
}
