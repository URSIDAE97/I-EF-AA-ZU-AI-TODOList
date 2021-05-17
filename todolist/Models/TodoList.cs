using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace todolist.Models
{
    [Table("todo_list")]
    public class TodoList
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 3)]
        public string Name { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("modified")]
        public DateTime Modified { get; set; }

        //
        // Foreign keys

        [ForeignKey("user_id")]
        [Column("user_id")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("category_id")]
        [Column("category_id")]
        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //
        // Relations

        public ICollection<TodoTask> Tasks { get; set; }
    }
}
