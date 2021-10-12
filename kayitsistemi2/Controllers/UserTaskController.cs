using kayitsistemi2.Areas.Identity.Data;
using kayitsistemi2.Data;
using kayitsistemi2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
namespace kayitsistemi2.Controllers
{
    [Route("usertask")]//buna bak
    [Authorize(Roles = "User")]
    public class UserTaskController : Controller
    {
        private readonly KayitdbContext context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserTaskController(KayitdbContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            _userManager = userManager;
        }



        public IActionResult Index()
        {
            //IQueryable<TaskModel> items = from i in context.TaskModels orderby i.TaskId select i;

            //List<TaskModel> todoList = await items.ToListAsync();

            //return View(todoList);
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> UserTask()
        {
            var current_User = await _userManager.GetUserAsync(HttpContext.User);
            var cr = current_User.Id;
            IQueryable<TaskModel> items = from i in context.TaskModels orderby i.TaskId select i;

            List<TaskModel> todoList = await items.Where(t => t.IdentityUserId == current_User.Id).ToListAsync();

            //string currentUserId = _userManager.Users..GetUserId();
            //ApplicationUser currentUser = _userManager.Users.FirstOrDefault(x => x.Id == currentUserId);
            //var current_User = await _userManager.GetUserAsync(HttpContext.User);


            // var tasks = context.TaskModels.Select(t => new TaskModel {  } ).Where(t => t.TaskId == ClaimTypes.)

            return View(todoList);
        }

        [HttpGet("do/{id}")]//buna bak
        public async Task<ActionResult> UserTask(int id)
        {
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var task = context.TaskModels.First(x => x.TaskId == id);
            task.TaskStatus = true;
            //task.IdentityUserId = userName;
            task.FinishTime = DateTime.Now;

            context.Update(task);
            await context.SaveChangesAsync().ConfigureAwait(false);//buna bak
            return RedirectToAction("Index");
        }
 
    }
}
