using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace todolist.Models
{
    [Table("user")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        [Required]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 3)]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special characters are not allowed")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("first_name")]
        [Required]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 3)]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special characters are not allowed")]
        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        [Required]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 3)]
        [RegularExpression(@"^[0-9a-zA-Z''-'\s]{1,40}$", ErrorMessage = "Special characters are not allowed")]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Column("email")]
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("modified")]
        [Display(Name = "Last modified")]
        public DateTime Modified { get; set; }

        //
        // Foreign keys

        [ForeignKey("role_id")]
        [Column("role_id")]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        //
        // Relations

        public ICollection<Category> Categories { get; set; }
        public ICollection<TodoList> TodoLists { get; set; }
    }
}
