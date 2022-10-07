using OniCore.Persistence.Repositories;

namespace OniCore.Security.Entities
{
    public class RefreshToken : Entity
    {
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Created { get; set; }
        public DateTime Expiration { get; set; }
        public string CreatedByIp { get; set; } = string.Empty;
        public DateTime? Revoked { get; set; }
        public string? RevokedByIp { get; set; }
        public string? ReplacedToken { get; set; }
        public string? RevokeReason { get; set; }

        public User? User { get; set; }
    }
}
