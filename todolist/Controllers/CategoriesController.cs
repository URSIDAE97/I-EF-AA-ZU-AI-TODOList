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
    public class CategoriesController : Controller
    {

        private readonly TodoListDbContext context;

        public CategoriesController(TodoListDbContext context)
        {
            this.context = context;
        }

        //
        // GET: /Categories/
        public async Task<IActionResult> Index()
        {
            return View(await context.Categories.ToListAsync());
        }

        //
        // GET: /Categories/Edit
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
        public IActionResult Edit(int? id, [Bind("Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    category.Created = DateTime.Now;
                    category.UserId = 1;
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
