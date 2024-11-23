using System.Security.Claims;

namespace Finances_Control_App_API.Middleware
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;

        public UserContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                context.Items["UserId"] = userId != null ? int.Parse(userId) : null;
            }

            await _next(context);
        }
    }
}
