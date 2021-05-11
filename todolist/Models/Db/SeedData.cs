using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using todolist.Models.Enums;

namespace todolist.Models.Db
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (
                var context = new TodoListDbContext(
                    serviceProvider.GetRequiredService<DbContextOptions<TodoListDbContext>>()
                )
            )
            {
                if (context.Roles.Any())
                {
                    return;   // DB has been already seeded
                }

                // Add predefined roles
                context.Roles.AddRange(
                    new Role
                    {
                        Id = 1,
                        Name = RolesEnum.USER
                    },
                    new Role
                    {
                        Id = 2,
                        Name = RolesEnum.ADMIN
                    }
                );

                context.SaveChanges();

                if (context.Users.Any())
                {
                    return; // DB has been already seeded
                }

                // Add predefined users
                context.Users.AddRange(
                    new User
                    {
                        Username = "admin",
                        FirstName = "Admin",
                        LastName = "Admin",
                        Password = "admin",
                        Email = "admin@admin.com",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        RoleId = 2
                    },
                    new User
                    {
                        Username = "user",
                        FirstName = "Jan",
                        LastName = "Nowak",
                        Password = "user",
                        Email = "jnowak@email.com",
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        RoleId = 1
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
