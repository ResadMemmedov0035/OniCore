namespace KodlamaDevs.Application.Features.Developers.DTOs
{
    public class GetDeveloperByIdDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string GithubAddress { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
