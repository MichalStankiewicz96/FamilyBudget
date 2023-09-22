using FamilyBudget.Application.Configuration.Options;
using FamilyBudget.Application.Services.JwtTokenServices.Dto;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FamilyBudget.Application.Services.JwtTokenServices;
public sealed class JwtTokenService : IJwtTokenService
{
    private const int ExpiresInMinutes = 30;
    private readonly JwtOptions _options;

    public JwtTokenService(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }

    public Token GenerateAccessToken(Guid userId)
    {
        var userClaims = GetUserClaims(userId);
        var jwtAuthRequiredClaims = GetJwtAuthRequiredClaims(_options.Issuer, _options.Audience);
        var claims = jwtAuthRequiredClaims.Union(userClaims);
        var key = Encoding.ASCII.GetBytes(_options.Secret);
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(ExpiresInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return new Token
        {
            AccessToken = tokenHandler.WriteToken(token),
            TokenType = "Bearer",
            ExpiresIn = ExpiresInMinutes * 60//in seconds
        };
    }

    private static IEnumerable<Claim> GetUserClaims(Guid userId)
    {
        return new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
        };
    }

    private static IEnumerable<Claim> GetJwtAuthRequiredClaims(string issuer, string audience)
    {
        return new[]
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iss, issuer),
            new Claim(JwtRegisteredClaimNames.Aud, audience)
        };
    }
}