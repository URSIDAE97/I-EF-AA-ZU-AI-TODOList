using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        //
        // Relations

        public ICollection<Task> Tasks { get; set; }
    }
}
