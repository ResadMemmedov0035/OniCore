using OniCore.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OniCore.Security.Tokens
{
    public interface ITokenService
    {
        public AccessToken CreateToken(User user);
        public RefreshToken CreateRefreshToken(User user, string ipAddress);
    }
}
