using OniCore.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ICollection<OperationClaim> OperationClaims { get; set; } = new HashSet<OperationClaim>();
        // RefreshTokens
        // AuthenticatorType
    }
}
