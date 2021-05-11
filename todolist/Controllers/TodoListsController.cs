using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todolist.Controllers
{
    public class TodoListsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
