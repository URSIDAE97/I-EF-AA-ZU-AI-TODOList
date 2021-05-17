using Microsoft.AspNetCore.Builder;

namespace todolist.Middleware
{
    public static class IdentityMiddlewareExtension
    {
        public static IApplicationBuilder UseIdentityMiddleware(
            this IApplicationBuilder builder
        )
        {
            return builder.UseMiddleware<IdentityMiddleware>();
        }
    }
}
