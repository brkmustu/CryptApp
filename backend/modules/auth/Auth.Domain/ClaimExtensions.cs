using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Auth;

public static class ClaimExtensions
{
    public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
    {
        claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
    }
}
