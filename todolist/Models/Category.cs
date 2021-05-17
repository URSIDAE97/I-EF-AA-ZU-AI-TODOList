using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace todolist.Models
{
    [Table("category")]
    public class Category
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
    }
}
