using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OniCore.Security.Encryption;
using OniCore.Security.Entities;
using OniCore.Security.Extensions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OniCore.Security.Tokens
{
    public class JwtManager : ITokenService
    {
        private readonly TokenOptions _tokenOptions;
        private DateTime _accessTokenExpiration;

        public JwtManager(IConfiguration configuration)
        {
            _tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
        }

        public AccessToken CreateToken(User user)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);

            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);

            var jwt = CreateJwtSecurityToken(user, _tokenOptions, signingCredentials);

            return new()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwt),
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(User user, TokenOptions tokenOptions,
                                                       SigningCredentials signingCredentials)
        {
            return new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                notBefore: DateTime.Now,
                expires: _accessTokenExpiration,
                signingCredentials: signingCredentials,
                claims: SetClaims(user)
                );
        }

        private static IEnumerable<Claim> SetClaims(User user)
        {
            List<Claim> claims = new();
            claims.AddNameIdentifier(user.Id.ToString());
            claims.AddEmail(user.Email);
            claims.AddRoles(user.OperationClaims.Select(x => x.Name).ToArray());
            claims.AddName($"{user.FirstName} {user.LastName}");
            return claims;
        }
    }
}
