using OniCore.Security.Entities;

namespace OniCore.Security.Tokens
{
    public interface ITokenService
    {
        public AccessToken CreateToken(User user);
        public RefreshToken CreateRefreshToken(User user, string ipAddress);
    }
}
