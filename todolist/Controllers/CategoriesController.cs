using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using todolist.Models.Db;
using todolist.Models;
using todolist.Middleware;

namespace todolist.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly TodoListDbContext context;

        public CategoriesController(TodoListDbContext context)
        {
            this.context = context;
        }

        //
        // GET: /Categories/
        [SignedIn]
        public IActionResult Index()
        {
            var identity = HttpContext.Items["Identity"];
            int currUserId = identity != null ? (int) identity : -1;
            var categories = context.Categories.Where(c => c.UserId == currUserId);
            return View(categories);
        }

        //
        // GET: /Categories/Edit
        [SignedIn]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new Category());
            }
            else
            {
                return View(context.Categories.Find(id));
            }
        }

        //
        // POST: /Categories/Edit
        [HttpPost]
        [SignedIn]
        public IActionResult Edit(int? id, [Bind("Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    category.Created = DateTime.Now;
                    int currUserId = (int) HttpContext.Items["Identity"];
                    category.UserId = currUserId;
                }
                category.Modified = DateTime.Now;

                context.Categories.Add(category);
                context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }
    }
}
