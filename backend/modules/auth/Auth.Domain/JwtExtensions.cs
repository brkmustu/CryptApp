using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth;

public static class JwtExtensions
{
    public static TokenResult CreateToken(
          this ApplicationIdentifier app,
          TokenOptions tokenOptions
      )
    {
        var accessTokenExpiration = DateTime.Now.AddMinutes(tokenOptions.AccessTokenExpiration);
        var securityKey = CreateSecurityKey(tokenOptions.SecurityKey);
        var signingCredentials = CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(tokenOptions, app, signingCredentials, accessTokenExpiration);
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
            DateTime accessTokenExpiration
        )
    {
        var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(app),
                signingCredentials: signingCredentials
        );
        return jwt;
    }

    private static IEnumerable<Claim> SetClaims(ApplicationIdentifier app)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(app.ApiKey);
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
