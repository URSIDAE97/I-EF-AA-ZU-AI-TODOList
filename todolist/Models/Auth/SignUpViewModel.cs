using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todolist.Models.Auth
{
    public class SignUpViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        public string Password { get; set; }
        [Display(Name = "Repeat password")]
        public string PasswordRep { get; set; }
    }
}
