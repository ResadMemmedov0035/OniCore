using KodlamaDevs.Application.Services.Repositories;
using KodlamaDevs.Domain.Entities;
using KodlamaDevs.Persistence.Contexts;
using OniCore.Persistence.Repositories;
using OniCore.Security.Entities;

namespace KodlamaDevs.Persistence.Repositories
{
    public class DeveloperRepository : EFCoreRepository<Developer>, IDeveloperRepository
    {
        public DeveloperRepository(ApplicationDbContext context) : base(context)
        {

        }

        public void SetRolesByClaimId(Developer developer, int[] claimsIds, bool saveChanges = true)
        {
            List<OperationClaim> claims = Context.Set<OperationClaim>().Where(c => claimsIds.Contains(c.Id)).ToList();
            claims.ForEach(developer.OperationClaims.Add);
            if (saveChanges)
                Context.SaveChanges();
        }
    }
}
