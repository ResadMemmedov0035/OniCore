using OniCore.Security.Entities;
using OniCore.Security.Tokens;
using System.Text.Json.Serialization;

namespace KodlamaDevs.Application.Features.Developers.DTOs
{
    public class AuthorizedDeveloperDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string GithubAddress { get; set; } = string.Empty;
        public AccessToken AccessToken { get; set; } = new();
        [JsonIgnore]
        public RefreshToken RefreshToken { get; set; } = new();
    }
}
