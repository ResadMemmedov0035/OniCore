namespace KodlamaDevs.Application.Features.Developers.DTOs
{
    public class RemovedDeveloperClaimDTO
    {
        public int ClaimId { get; set; }
        public int DeveloperId { get; set; }
        public string ClaimName { get; set; } = string.Empty;
    }
}
