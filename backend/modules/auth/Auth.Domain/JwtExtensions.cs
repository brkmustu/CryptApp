using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth;

public static class JwtExtensions
{
    public static TokenResult CreateToken(
          this ApplicationIdentifier app,
          IEnumerable<string> permissions,
          TokenOptions tokenOptions
      )
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);
        var securityKey = CreateSecurityKey(tokenOptions.SecurityKey);
        var signingCredentials = CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(tokenOptions, app, signingCredentials, permissions, accessTokenExpiration);
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var token = jwtSecurityTokenHandler.WriteToken(jwt);

        return new TokenResult()
        {
            Token = token,
            Expiration = accessTokenExpiration
        };
    }

    internal static JwtSecurityToken CreateJwtSecurityToken(
            TokenOptions tokenOptions,
            ApplicationIdentifier app,
            SigningCredentials signingCredentials,
            IEnumerable<string> permissions,
            DateTime accessTokenExpiration
        )
    {
        var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(app, permissions),
                signingCredentials: signingCredentials
        );
        return jwt;
    }

    private static IEnumerable<Claim> SetClaims(ApplicationIdentifier app, IEnumerable<string> permissions)
    {
        var claims = new List<Claim>();
        claims.AddName(app.Name);
        claims.AddNameIdentifier(app.Id.ToString());
        claims.AddRoles(permissions.ToArray());

        return claims;
    }

    internal static SecurityKey CreateSecurityKey(string securityKey)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
    }

    internal static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
    {
        return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
    }
}
