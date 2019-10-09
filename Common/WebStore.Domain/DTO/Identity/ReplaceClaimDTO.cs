using System.Security.Claims;

namespace WebStore.Domain.DTO.Identity
{
    public class ReplaceClaimDTO : ClaimInfoDTO
    {
        public Claim OldClaim { get; set; }

        public Claim NewClaim { get; set; }
    }
}