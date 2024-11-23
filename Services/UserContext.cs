using Finances_Control_App_API.Services.Interfaces;

namespace Finances_Control_App_API.Services
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.Items["UserId"] as int?;

            if (userId == null)
            {
                throw new UnauthorizedAccessException("User not authorized.");
            }

            return userId.Value;
        }
    }
}
