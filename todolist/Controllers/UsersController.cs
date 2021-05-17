using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using todolist.Models.Db;
using Microsoft.EntityFrameworkCore;
using todolist.Models;
using todolist.Middleware;

namespace todolist.Controllers
{
    public class UsersController : Controller
    {

        private readonly TodoListDbContext context;

        public UsersController(TodoListDbContext context)
        {
            this.context = context;
        }

        // 
        // GET: /Users/
        [Admin]
        public async Task<IActionResult> Index()
        {
            return View(await context.Users.ToListAsync());
        }

        //
        // GET: /Users/Details/
        [CurrentUser]
        public IActionResult Details(int id)
        {
            var user = context.Users
                .Include(u => u.Categories)
                .Include(u => u.TodoLists)
                .First(u => u.Id == id);
            return View(user);
        }

        //
        // GET: /Users/Edit/
        [CurrentUser]
        public IActionResult Edit(int id)
        {
            var user = context.Users.First(u => u.Id == id);
            return View(user);
        }

        //
        // POST: /Users/Edit/
        [HttpPost]
        [CurrentUser]
        public IActionResult Edit(int? id, [Bind("Username,FirstName,LastName,Email")] User model)
        {
            if (ModelState.IsValid)
            {
                if (context.Users.Count(u => u.Username == model.Username) > 1)
                {
                    ModelState.AddModelError("NonUniqueUsername", "This username is already in use");
                    return View(model);
                }

                var user = context.Users.First(u => u.Id == id);
                user.Username = model.Username;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Modified = DateTime.Now;

                context.Users.Update(user);
                context.SaveChanges();

                return RedirectToAction(nameof(Details), new { id = user.Id });
            }
            return View(model);
        }

        //
        // GET: /Users/Delete/
        [Admin]
        public IActionResult Delete(int id)
        {
            var user = context.Users.First(u => u.Id == id);
            return View(user);
        }

        //
        // POST: /Users/Delete/
        [HttpPost, ActionName("Delete")]
        [Admin]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = context.Users.Find(id);
            context.Users.Remove(user);
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
