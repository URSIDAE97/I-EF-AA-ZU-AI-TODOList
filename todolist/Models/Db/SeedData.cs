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
                if (!context.Roles.Any())
                {
                    // Add predefined roles
                    context.Roles.AddRange(
                        new Role
                        {
                            Id = RolesEnum.USER_ID,
                            Name = RolesEnum.USER
                        },
                        new Role
                        {
                            Id = RolesEnum.ADMIN_ID,
                            Name = RolesEnum.ADMIN
                        }
                    );

                    context.SaveChanges();
                }

                if (!context.Users.Any())
                {
                    // Add predefined users
                    context.Users.AddRange(
                        new User
                        {
                            Username = "admin",
                            FirstName = "Admin",
                            LastName = "Admin",
                            Password = BCrypt.Net.BCrypt.HashPassword("admin"),
                            Email = "admin@admin.com",
                            Created = DateTime.Now,
                            Modified = DateTime.Now,
                            RoleId = RolesEnum.ADMIN_ID
                        },
                        new User
                        {
                            Username = "user",
                            FirstName = "Jan",
                            LastName = "Nowak",
                            Password = BCrypt.Net.BCrypt.HashPassword("user"),
                            Email = "jnowak@email.com",
                            Created = DateTime.Now,
                            Modified = DateTime.Now,
                            RoleId = RolesEnum.USER_ID
                        }
                    );

                    context.SaveChanges();
                }
            }
        }
    }
}
