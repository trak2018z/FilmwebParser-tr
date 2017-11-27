using System.Security.Claims;
using System.Security.Principal;

namespace FilmwebParser.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetAvatar(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("Avatar");
            return (claim != null) ? claim.Value : "http://funkyimg.com/i/2zK9A.png";
        }
    }
}
