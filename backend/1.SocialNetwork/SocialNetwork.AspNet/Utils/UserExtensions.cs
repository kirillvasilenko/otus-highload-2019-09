using System.Security.Claims;

namespace SocialNetwork.AspNet.Utils
{
    public static class UserExtensions
    {
        public static long GetId(this ClaimsPrincipal user)
        {
            if (!long.TryParse(user.Identity.Name, out long result))
            {
                result = -1;
            }

            return result;
        }
    }
}