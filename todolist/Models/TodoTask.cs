using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace todolist.Models
{
    [Table("task")]
    public class TodoTask
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(50, ErrorMessage = "{0} length must be between {2} and {1}", MinimumLength = 3)]
        public string Name { get; set; }

        [Column("priority")]
        [Required]
        [Range(1, 10, ErrorMessage = "{0} must be between {1} and {2}")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "{0} must be an integer")]
        public int Priority { get; set; }

        [Column("deadline")]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }

        [Column("created")]
        public DateTime Created { get; set; }

        [Column("modified")]
        public DateTime Modified { get; set; }

        [Column("completed")]
        [Display(Name = "Is completed")]
        public bool IsCompleted { get; set; }

        //
        // Foreign keys

        [ForeignKey("todolist_id")]
        [Column("todolist_id")]
        public int TodoListId { get; set; }

    }
}
