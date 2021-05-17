using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using todolist.Models.Db;
using Microsoft.EntityFrameworkCore;
using todolist.Models;

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
        public async Task<IActionResult> Index()
        {
            return View(await context.Users.ToListAsync());
        }

        //
        // GET: /Users/Details/
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
        public IActionResult Edit(int id)
        {
            var user = context.Users.First(u => u.Id == id);
            return View(user);
        }

        //
        // POST: /Users/Edit/
        [HttpPost]
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
    }
}
