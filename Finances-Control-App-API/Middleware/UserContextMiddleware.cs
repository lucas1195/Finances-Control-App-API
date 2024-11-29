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

                var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(userIdClaim) && int.TryParse(userIdClaim, out var userId))
                {
                    context.Items["UserId"] = userId;
                }
                else
                {
                    throw new UnauthorizedAccessException("UserId is missing or invalid.");
                }
            }

            await _next(context);
        }
    }
}
