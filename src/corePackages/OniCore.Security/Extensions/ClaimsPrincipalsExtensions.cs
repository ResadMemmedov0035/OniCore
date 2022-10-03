using System.Security.Claims;

namespace OniCore.Security.Extensions
{
    public static class ClaimsPrincipalsExtensions
    {
        public static List<string> GetClaims(this ClaimsPrincipal claimsPrincipal, string type)
        {
            return claimsPrincipal.FindAll(type).Select(x => x.Value).ToList();
        }

        public static List<string> GetRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.GetClaims(ClaimTypes.Role);
        }

        public static int GetId(this ClaimsPrincipal claimsPrincipal)
        {
            return Convert.ToInt32(claimsPrincipal.GetClaims(ClaimTypes.NameIdentifier).FirstOrDefault());
        }
    }
}
