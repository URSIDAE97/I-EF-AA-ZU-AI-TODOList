using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todolist.Models;
using todolist.Models.Auth;
using todolist.Models.Db;
using todolist.Models.Enums;

namespace todolist.Controllers
{
    public class AuthController : Controller
    {
        private readonly TodoListDbContext context;

        public AuthController(TodoListDbContext context)
        {
            this.context = context;
        }

        //
        // GET: /Auth/SignIn
        public IActionResult SignIn()
        {
            SignInViewModel LoginData = new SignInViewModel();
            return View(LoginData);
        }

        //
        // POST: /Auth/SignIn
        [HttpPost]
        public IActionResult SignIn([Bind("Login,Password")] SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = context.Users.Where(u => u.Username.Equals(model.Username)).FirstOrDefault();
                if (user != null && user.Password.Equals(model.Password))
                {
                    return Redirect("/Home");
                }

            }
            return View(model);
        }

        //
        // GET: /Auth/SignUp
        public IActionResult SignUp()
        {
            return View();
        }

        //
        // POST: /Auth/SignUp
        [HttpPost]
        public IActionResult SignUp([Bind("Username,Email,FirstName,LastName,Password,PasswordRep")] SignUpViewModel model)
        {
            if (ModelState.IsValid)
            {
                User newUser = new User();
                newUser.Username = model.Username;
                newUser.FirstName = model.FirstName;
                newUser.LastName = model.LastName;
                newUser.Email = model.Email;
                newUser.Password = model.Password;

                newUser.RoleId = RolesEnum.USER_ID;
                newUser.Created = DateTime.Now;
                newUser.Modified = DateTime.Now;

                context.Users.Add(newUser);
                context.SaveChanges();
            }
            return Redirect("/Home");
        }
    }
}
