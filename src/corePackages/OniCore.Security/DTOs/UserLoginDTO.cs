using OniCore.Security.Enums;

namespace OniCore.Security.DTOs
{
    public class UserLoginDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? AuthenticatorCode { get; set; }
    }
}
