using System.Security.Claims;

namespace FamilyBudget.Api.Extensions.Auth;

public static class ClaimsExtensions
{
    public static Guid GetUserId(this IEnumerable<Claim> claims)
    {
        var claim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        if (claim is null)
        {
            throw new ArgumentException("claim not found");
        }
        return new Guid(claim.Value);
    }
}