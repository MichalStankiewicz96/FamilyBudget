using FamilyBudget.Application.Behaviour.Exceptions;
using FamilyBudget.Application.Behaviour.Exceptions.ErrorCode;
using FamilyBudget.Persistence;
using FamilyBudget.Persistence.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Application.Requests.Users.Commands.CreateUser;
public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly ApplicationDbContext _context;

    public CreateUserCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await _context.Users.AnyAsync(x => x.Name == request.Name, cancellationToken))
        {
            throw new VerificationException($"Name {request.Name} is taken", ErrorCodes.User.NameTaken);
        }
        var userEntity = new UserEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };
        _context.Users.Add(userEntity);
        await _context.SaveChangesAsync(cancellationToken);
        return userEntity.Id;
    }
}