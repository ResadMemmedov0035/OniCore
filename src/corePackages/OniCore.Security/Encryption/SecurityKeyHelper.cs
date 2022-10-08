using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OniCore.Security.Encryption
{
    public class SecurityKeyHelper
    {
        public static SymmetricSecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
