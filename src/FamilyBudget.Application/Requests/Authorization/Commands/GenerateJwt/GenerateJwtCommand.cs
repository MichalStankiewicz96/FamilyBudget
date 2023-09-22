using MediatR;

namespace FamilyBudget.Application.Requests.Authorization.Commands.GenerateJwt;
public sealed class GenerateJwtCommand : IRequest<AuthenticationDto>
{
    public Guid UserId { get; set; }
}