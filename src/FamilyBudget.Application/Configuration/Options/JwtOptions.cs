namespace FamilyBudget.Application.Configuration.Options;
public sealed class JwtOptions
{
    public required string Secret { get; set; }
    public required string Audience { get; set; }
    public required string Issuer { get; set; }
}