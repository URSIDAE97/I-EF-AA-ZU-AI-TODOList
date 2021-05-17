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
        public IActionResult Edit(int? id, [Bind("Name")] Category model)
        {
            if (ModelState.IsValid)
            {
                var identity = HttpContext.Items["Identity"];
                int currUserId = identity != null ? (int) identity : -1;
                int categoriesCount = context.Categories
                    .Count(c => c.Name == model.Name && c.UserId == currUserId);
                if ((id == null && categoriesCount > 0) || (id != null && categoriesCount > 1))
                {
                    ModelState.AddModelError("NonUniqueCategory", "Provided category is not unique");
                    return View(model);
                }
                Category category;
                if (id == null)
                {
                    category = new Category();
                    category.UserId = currUserId;
                    category.Created = DateTime.Now;
                }
                else
                {
                    category = context.Categories.Find(id);
                }
                category.Name = model.Name;
                category.Modified = DateTime.Now;

                if (id == null)
                {
                    context.Categories.Add(category);
                }
                else
                {
                    context.Categories.Update(category);
                }
                context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        //
        // GET: /Categories/Delete/
        [SignedIn]
        public IActionResult Delete(int id)
        {
            return View(context.Categories.Find(id));
        }

        //
        // POST: /Categories/Delete/
        [HttpPost, ActionName("Delete")]
        [SignedIn]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = context.Categories.Find(id);
            context.Categories.Remove(category);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
