using OniCore.Persistence.Repositories;

namespace KodlamaDevs.Domain.Entities
{
    public class Technology : Entity
    {
        public int ProgrammingLanguageId { get; set; }
        public string Name { get; set; }

        public ProgrammingLanguage ProgrammingLanguage { get; set; }
    }
}
