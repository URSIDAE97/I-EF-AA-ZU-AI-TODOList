using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Encodings.Web;
using todolist.Models.Db;
using Microsoft.EntityFrameworkCore;

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
    }
}
