using kayitsistemi2.Areas.Identity.Data;
using kayitsistemi2.Data;
using kayitsistemi2.Models;
using kayitsistemi2.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace kayitsistemi2.Controllers
{
    //[Authorize(Policy = "RequireAdministratorRole")]
    [Authorize(Roles = "Admin")]
    public class ToDoController : Controller
    {

        //private readonly RoleManager<IdentityRole> roleManager;
        //private readonly RoleManager<IdentityRole> roleManager;

        //public AdminController(RoleManager<IdentityRole> roleManager)
        //{
        //    this.roleManager = roleManager;
        //}
        //private readonly IIUnitOfWork unitOfWork;

        //public ToDoController(IIUnitOfWork unitOfWork)
        //{
        //    this.unitOfWork = unitOfWork;
        //}
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly KayitdbContext context;

        public ToDoController(KayitdbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }

        // GET /
        public async Task<ActionResult> Index()
        {

            //var deneme = from user in context.ApplicationUsers
            //             join c in context.TaskModels
            //             on user.Id equals c.IdentityUserId select (user.FirstName+user.LastName).Distinct().ToList();

            

            var deneme2 = (from user in context.ApplicationUsers
                          join us in context.TaskModels
                          on user.Id equals us.IdentityUserId
                          select user).Distinct();
            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);            
            ViewBag.Kullanici = deneme2;
            //var ad = from task in context.TaskModels
            //         where task.IdentityUserId == (_userManager.Users.)
            //var userAndRoles = User.Select(user => new
            //{
            //    UserName = user.UserName,
            //    Roles = roles.Where(role => user.Roles.Any(userRole => userRole.UserId == user.UserId && userRole.RoleId == role.RoleId))
            //});

            #region eski yöntem çalışır
            IQueryable<TaskModel> items = from i in context.TaskModels orderby i.TaskId select i;
            List<TaskModel> todoList = await items.ToListAsync();
            return View(todoList);
            #endregion
            //return View(deneme);

        }
        [HttpGet]
        public IActionResult Create()
        {
            //Yeni eklendi view a veri gidecek.
            ViewBag.Users = _userManager.Users;
            return View();
        }


        public async Task<ActionResult> Create(TaskModel taskModel, IFormCollection form)
        { 
            string userId = form["userId"];
            taskModel.IdentityCreatorId = User.FindFirstValue(ClaimTypes.Name);
            taskModel.CreateTime = DateTime.Now;
            taskModel.FinishTime = null;
            taskModel.IdentityUserId = userId;
            context.Add(taskModel);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
        public async Task<ActionResult> Delete(TaskModel taskModel)
        {
            context.Remove(taskModel);
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            TaskModel item = await context.TaskModels.FindAsync(id);
            if (item == null)
            {
                TempData["Error"] = "The item does not exist!";
            }
            else
            {
                context.TaskModels.Remove(item);
                await context.SaveChangesAsync();

                TempData["Success"] = "The item has been deleted!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Users = _userManager.Users;
            TaskModel item = await context.TaskModels.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TaskModel item, IFormCollection form)
        {
            if (ModelState.IsValid)
            {
                string userId = form["userId"];
                var userName = User.FindFirstValue(ClaimTypes.Name);
                item.IdentityCreatorId = userName;
                item.IdentityUserId = userId;
                item.CreateTime = DateTime.Now;
                context.Update(item);
                await context.SaveChangesAsync();
              
                TempData["Success"] = "The item has been updated!";

                return RedirectToAction("Index");
            }

            return View(item);
        }  
    }
}
