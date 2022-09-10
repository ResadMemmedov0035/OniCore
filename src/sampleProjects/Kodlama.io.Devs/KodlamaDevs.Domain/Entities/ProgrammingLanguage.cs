using OniCore.Persistence.Repositories;

namespace KodlamaDevs.Domain.Entities
{
    public class ProgrammingLanguage : Entity
    {
        public string Name { get; set; }

        public ICollection<Technology> Technologies { get; set; }
    }
}
