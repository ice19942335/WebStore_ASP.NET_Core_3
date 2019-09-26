using System.Security.Claims;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Domain.Identity
{
    public class ReplaceClaimDTO : ClaimInfoDTO
    {
        public Claim OldClaim { get; set; }

        public Claim NewClaim { get; set; }
    }
}