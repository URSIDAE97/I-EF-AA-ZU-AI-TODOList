using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace todolist.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate next;

        public AuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            byte[] bytes;
            bool loggedIn = context.Session.TryGetValue("Identity", out bytes);
            context.Items.Add("IsLoggedIn", loggedIn);
            if (loggedIn)
            {
                string identity = Encoding.UTF8.GetString(bytes);
                string[] elements = identity.Split(";");
                context.Items.Add("Identity", elements[0]);
                context.Items.Add("FirstName", elements[1]);
                context.Items.Add("LastName", elements[2]);

                if (
                    context.Request.Path != "/Auth/SignIn" &&
                    context.Request.Path != "/Auth/SignUp"
                )
                {
                    await next(context);
                }
                else
                {
                    context.Response.Redirect("/Home");
                }
            }
            else if (
                context.Request.Path != "/Auth/SignIn" &&
                context.Request.Path != "/Auth/SignUp"
            )
            {
                context.Response.Redirect("/Auth/SignIn");
            }
            else
            {
                await next(context);
            }
        }
    }
}
