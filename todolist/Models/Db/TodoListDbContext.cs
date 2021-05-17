using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todolist.Models;

namespace todolist.Models.Db
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext (DbContextOptions<TodoListDbContext> options)
            : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<TodoList> TodoLists { get; set; }
        public DbSet<TodoTask> Tasks { get; set; }
    }
}
