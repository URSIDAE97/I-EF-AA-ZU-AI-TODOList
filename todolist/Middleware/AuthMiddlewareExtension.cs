using Microsoft.AspNetCore.Builder;

namespace todolist.Middleware
{
    public static class AuthMiddlewareExtension
    {
        public static IApplicationBuilder UseAuthMiddleware(
            this IApplicationBuilder builder
        )
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
