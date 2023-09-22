using FamilyBudget.Application.Services.JwtTokenServices.Dto;

namespace FamilyBudget.Application.Services.JwtTokenServices;
public interface IJwtTokenService
{
    public Token GenerateAccessToken(Guid userId);
}