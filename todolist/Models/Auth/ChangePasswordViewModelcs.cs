using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace todolist.Models.Auth
{
    public class ChangePasswordViewModelcs
    {
        public int UserId { get; set; }

        [Required]
        [Display(Name = "Old password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 3)]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 3)]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Repeat new password")]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 3)]
        public string NewPasswordRep { get; set; }
    }
}
