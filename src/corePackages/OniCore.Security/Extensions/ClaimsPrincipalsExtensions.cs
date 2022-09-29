using System.Security.Claims;

namespace OniCore.Security.Extensions
{
    public static class ClaimsPrincipalsExtensions
    {
        public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string type)
        {
            return claimsPrincipal.FindAll(type).Select(x => x.Value).ToList();
        }

        public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims(ClaimTypes.Role);
        }

        public static int ClaimId(this ClaimsPrincipal claimsPrincipal)
        {
            return Convert.ToInt32(claimsPrincipal.Claims(ClaimTypes.NameIdentifier).FirstOrDefault());
        }
    }
}
