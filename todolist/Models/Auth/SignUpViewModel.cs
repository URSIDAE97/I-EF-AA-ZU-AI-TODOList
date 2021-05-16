using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todolist.Models.Auth
{
    public class SignUpViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 3)]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special characters are not allowed")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 3)]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special characters are not allowed")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 3)]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special characters are not allowed")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 3)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Repeat password")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 3)]
        public string PasswordRep { get; set; }
    }
}
