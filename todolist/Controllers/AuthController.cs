using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public IActionResult SignIn([Bind("Username,Password")] SignInViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = context.Users.First(u => u.Username.Equals(model.Username));
                if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    HttpContext.Session.Set(
                        "Identity",
                        Encoding.UTF8.GetBytes(string.Format("{0};{1};{2}", user.Id, user.FirstName, user.LastName))
                    );
                    return Redirect("/Home");
                }
                else
                {
                    ModelState.AddModelError("SignInFailed", "Incorect username or password");
                    return View(model);
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
                if (context.Users.Any(u => u.Username == model.Username))
                {
                    ModelState.AddModelError("NonUniqueUsername", "This username is already in use");
                    return View(model);
                }

                User newUser = new User();
                newUser.Username = model.Username;
                newUser.FirstName = model.FirstName;
                newUser.LastName = model.LastName;
                newUser.Email = model.Email;
                newUser.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
                newUser.RoleId = RolesEnum.USER_ID;
                newUser.Created = DateTime.Now;
                newUser.Modified = DateTime.Now;

                context.Users.Add(newUser);
                context.SaveChanges();

                return Redirect("/Home");
            }
            return View(model);
        }

        //
        // GET: /Auth/LogOut
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();

            return RedirectToAction(nameof(SignIn));
        }
    }
}
