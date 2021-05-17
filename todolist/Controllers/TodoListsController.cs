using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using todolist.Models.Db;
using todolist.Models;
using todolist.Middleware;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace todolist.Controllers
{
    public class TodoListsController : Controller
    {
        private readonly TodoListDbContext context;

        public TodoListsController(TodoListDbContext context)
        {
            this.context = context;
        }

        //
        // GET: /TodoLists/
        [SignedIn]
        public IActionResult Index()
        {
            var identity = HttpContext.Items["Identity"];
            int currUserId = identity != null ? (int) identity : -1;
            var lists = context.TodoLists
                .Include(t => t.Category)
                .Where(t => t.UserId == currUserId);
            return View(lists);
        }

        //
        // GET: /TodoLists/Edit
        [SignedIn]
        public IActionResult Edit(int? id)
        {
            var identity = HttpContext.Items["Identity"];
            int currUserId = identity != null ? (int) identity : -1;
            ViewData["Categories"] = context.Categories
                .Where(c => c.UserId == currUserId)
                .ToList();
            if (id == null)
            {
                return View(new TodoList());
            }
            else
            {
                return View(context.TodoLists.Find(id));
            }
        }

        //
        // POST: /TodoLists/Edit
        [HttpPost]
        [SignedIn]
        public IActionResult Edit(int? id, [Bind("Name,CategoryId")] TodoList model)
        {
            if (ModelState.IsValid)
            {
                var identity = HttpContext.Items["Identity"];
                int currUserId = identity != null ? (int)identity : -1;
                int listsCount = context.TodoLists
                    .Count(t => t.Name == model.Name && t.UserId == currUserId);
                if ((id == null && listsCount > 0) || (id != null && listsCount > 1))
                {
                    ModelState.AddModelError("NonUniqueName", "Provided name is not unique");
                    return View(model);
                }
                TodoList list;
                if (id == null)
                {
                    list = new TodoList();
                    list.UserId = currUserId;
                    list.Created = DateTime.Now;
                }
                else
                {
                    list = context.TodoLists.Find(id);
                }
                list.Name = model.Name;
                list.CategoryId = model.CategoryId;
                list.Modified = DateTime.Now;

                if (id == null)
                {
                    context.TodoLists.Add(list);
                }
                else
                {
                    context.TodoLists.Update(list);
                }
                context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        //
        // GET: /TodoLists/Manage/
        [SignedIn]
        public IActionResult Manage(int id)
        {
            var list = context.TodoLists
                .Include(t => t.Category)
                .Include(t => t.Tasks)
                .First(t => t.Id == id);
            return View(list);
        }

        //
        // GET: /TodoLists/Delete/
        [SignedIn]
        public IActionResult Delete(int id)
        {
            var list = context.TodoLists
                .Include(t => t.Category)
                .First(t => t.Id == id);
            return View(list);
        }

        //
        // POST: /TodoLists/Delete/
        [HttpPost, ActionName("Delete")]
        [SignedIn]
        public IActionResult DeleteConfirmed(int id)
        {
            var list = context.TodoLists.Find(id);
            context.TodoLists.Remove(list);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
