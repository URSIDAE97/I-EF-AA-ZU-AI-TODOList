using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todolist.Models.Db;
using Microsoft.EntityFrameworkCore;
using todolist.Models;

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
        public IActionResult Index()
        {
            return View(context.TodoLists.ToList());
        }

        //
        // GET: /TodoLists/Edit
        public IActionResult Edit(int? id)
        {
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
        public IActionResult Edit(int? id, [Bind("Name")] TodoList list)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    list.Created = DateTime.Now;
                    list.UserId = 1;
                }
                list.Modified = DateTime.Now;

                context.TodoLists.Add(list);
                context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(list);
        }
    }
}
