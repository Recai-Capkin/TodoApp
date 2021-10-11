﻿using kayitsistemi2.Data;
using kayitsistemi2.Models;
using kayitsistemi2.Repository;
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

        private readonly KayitdbContext context;

        public ToDoController(KayitdbContext context)
        {
            this.context = context;
        }

        // GET /
        public async Task<ActionResult> Index()
        {
            IQueryable<TaskModel> items = from i in context.TaskModels orderby i.TaskId select i;

            List<TaskModel> todoList = await items.ToListAsync();

            return View(todoList);

        }
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<ActionResult> Create(TaskModel taskModel)
        {
            taskModel.IdentityCreatorId = User.FindFirstValue(ClaimTypes.Name);
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
            TaskModel item = await context.TaskModels.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(TaskModel item)
        {
            if (ModelState.IsValid)
            {
                var userName = User.FindFirstValue(ClaimTypes.Name);
                item.IdentityCreatorId = userName;

                context.Update(item);
                await context.SaveChangesAsync();
              
                TempData["Success"] = "The item has been updated!";

                return RedirectToAction("Index");
            }

            return View(item);
        }  
    }
}