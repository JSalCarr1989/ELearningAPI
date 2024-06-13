using System.Security.Claims;

namespace ELearningAPI.Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst("UserId");

            return userIdClaim == null ? Guid.Parse(userIdClaim.Value) : Guid.Empty;
        }
    }
}
