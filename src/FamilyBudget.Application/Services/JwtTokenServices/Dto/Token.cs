namespace FamilyBudget.Application.Services.JwtTokenServices.Dto;
public sealed class Token
{
    public required string AccessToken { get; set; }
    public required string TokenType { get; set; }
    public required int ExpiresIn { get; set; }
}