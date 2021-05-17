using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace todolist.Middleware
{
    public class CurrentUserAttribute : ResultFilterAttribute
    {
        public CurrentUserAttribute()
        {
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            var identity = context.HttpContext.Items["Identity"];
            int currUserId = identity != null ? (int) identity : -1;
            int requestedUserId = Int32.Parse(context.HttpContext.Request.Path.ToString().Split("/")[3]);
            var admin = context.HttpContext.Items["IsAdmin"];
            bool isAdmin = admin != null ? (bool)admin : false;
            if (currUserId.Equals(requestedUserId) || isAdmin)
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
