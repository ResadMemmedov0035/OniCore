using OniCore.Persistence.Repositories;
using OniCore.Security.Enums;

namespace OniCore.Security.Entities
{
    public class User : Entity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();
        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();
        public bool Status { get; set; } = true;
        public AuthenticatorType AuthenticatorType { get; set; }

        public ICollection<OperationClaim> OperationClaims { get; set; } = new HashSet<OperationClaim>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new HashSet<RefreshToken>();
    }
}
