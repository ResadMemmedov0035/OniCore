using OniCore.Security.Entities;
using OniCore.Security.Enums;
using OniCore.Security.Tokens;

namespace KodlamaDevs.Application.Features.Developers.DTOs
{
    public class LoggedDeveloperDTO
    {
        public UIModel Model { get; set; } = new();
        public RefreshToken? RefreshToken { get; set; }

        public class UIModel
        {
            public AccessToken? AccessToken { get; set; }
            public AuthenticatorType? RequiredAuthenticatorType { get; set; }
        }
    }
}
