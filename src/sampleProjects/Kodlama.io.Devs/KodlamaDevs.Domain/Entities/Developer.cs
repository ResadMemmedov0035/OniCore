using OniCore.Security.Entities;

namespace KodlamaDevs.Domain.Entities
{
    public class Developer : User
    {
        public string GithubAddress { get; set; }
    }
}
