namespace KodlamaDevs.Application.Features.Technologies.DTOs
{
    public class GetTechnologyByIdDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ProgrammingLanguageName { get; set; } = string.Empty;
    }
}
