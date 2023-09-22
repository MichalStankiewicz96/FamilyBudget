namespace FamilyBudget.Application.Requests.Authorization;
public sealed class AuthenticationDto
{
    public required string AccessToken { get; set; }
    public required string TokenType { get; set; }
    public required int ExpiresIn { get; set; }
}
