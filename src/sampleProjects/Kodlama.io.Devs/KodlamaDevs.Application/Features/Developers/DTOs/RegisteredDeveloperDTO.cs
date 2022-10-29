using OniCore.Security.Entities;
using OniCore.Security.Tokens;

namespace KodlamaDevs.Application.Features.Developers.DTOs
{
    public class RegisteredDeveloperDTO
    {
        public int Id { get; set; }
        public AccessToken AccessToken { get; set; } = new();
        public RefreshToken RefreshToken { get; set; } = new();
    }
}
