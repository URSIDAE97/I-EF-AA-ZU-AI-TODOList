using Microsoft.AspNetCore.Http;
using System;
using System.Text;
using System.Threading.Tasks;
using todolist.Models.Enums;

namespace todolist.Middleware
{
    public class IdentityMiddleware
    {
        private readonly RequestDelegate next;

        public IdentityMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            byte[] bytes;
            bool loggedIn = context.Session.TryGetValue("Identity", out bytes);
            context.Items.Add("IsSignedIn", loggedIn);
            if (loggedIn)
            {
                string identity = Encoding.UTF8.GetString(bytes);
                string[] elements = identity.Split(";");
                context.Items.Add("Identity", Int32.Parse(elements[0]));
                context.Items.Add("FirstName", elements[1]);
                context.Items.Add("LastName", elements[2]);
                context.Items.Add("IsAdmin", Int32.Parse(elements[3]).Equals(RolesEnum.ADMIN_ID));
            }

            await next(context);
        }
    }
}
