using Microsoft.AspNetCore.Mvc.Filters;

namespace todolist.Middleware
{
    public class SignedOutAttribute : ResultFilterAttribute
    {
        public SignedOutAttribute()
        {
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            bool isSignedOut = !(bool) context.HttpContext.Items["IsSignedIn"];
            if (isSignedOut)
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
