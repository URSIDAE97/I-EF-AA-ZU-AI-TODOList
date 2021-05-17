using Microsoft.AspNetCore.Mvc.Filters;

namespace todolist.Middleware
{
    public class AdminAttribute : ResultFilterAttribute
    {
        public AdminAttribute()
        {
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var admin = context.HttpContext.Items["IsAdmin"];
            bool isAdmin = admin != null ? (bool) admin : false;
            if (isAdmin)
            {
                base.OnResultExecuting(context);
            }
            else
            {
                context.HttpContext.Response.Redirect("/Home");
            }
        }
    }
}
