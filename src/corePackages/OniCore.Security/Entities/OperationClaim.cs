using OniCore.Persistence.Repositories;

namespace OniCore.Security.Entities
{
    public class OperationClaim : Entity
    {
        public string Name { get; set; } = string.Empty;

        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
