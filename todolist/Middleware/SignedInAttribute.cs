using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace todolist.Middleware
{
    public class SignedInAttribute : ResultFilterAttribute
    {
        public SignedInAttribute()
        {
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            bool isSignedIn = (bool)context.HttpContext.Items["IsSignedIn"];
            if (isSignedIn)
            {
                base.OnResultExecuting(context);
            }
            else
            {
                context.HttpContext.Response.Redirect("/Auth/SignIn");
            }
        }
    }
}
