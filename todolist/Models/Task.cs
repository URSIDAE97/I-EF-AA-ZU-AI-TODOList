using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace todolist.Models
{
    [Table("task")]
    public class Task
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("priority")]
        public int Priority { get; set; }
        [Column("deadline")]
        public DateTime Deadline { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("modified")]
        public DateTime Modified { get; set; }
        [Column("completed")]
        public bool IsCompleted { get; set; }

        //
        // Foreign keys

        [ForeignKey("todolist_id")]
        [Column("todolist_id")]
        public int TodoListId { get; set; }

    }
}
