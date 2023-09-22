using MediatR;

namespace FamilyBudget.Application.Requests.Users.Commands.CreateUser;
public sealed class CreateUserCommand : IRequest<Guid>
{
    public required string Name { get; set; }
}