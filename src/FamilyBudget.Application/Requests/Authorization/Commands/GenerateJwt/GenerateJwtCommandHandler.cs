using FamilyBudget.Application.Behaviour.Exceptions;
using FamilyBudget.Application.Services.JwtTokenServices;
using FamilyBudget.Persistence;
using FamilyBudget.Persistence.Entities.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.Application.Requests.Authorization.Commands.GenerateJwt;
public sealed class GenerateJwtCommandHandler : IRequestHandler<GenerateJwtCommand, AuthenticationDto>
{
    private readonly ApplicationDbContext _context;
    private readonly IJwtTokenService _jwtTokenService;

    public GenerateJwtCommandHandler(ApplicationDbContext context, IJwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<AuthenticationDto> Handle(GenerateJwtCommand request, CancellationToken cancellationToken)
    {
        if (!await _context.Users.AnyAsync(x => x.Id == request.UserId, cancellationToken))
        {
            throw new NotFoundException(typeof(UserEntity));
        }
        var accessToken = _jwtTokenService.GenerateAccessToken(request.UserId);
        return new AuthenticationDto
        {
            AccessToken = accessToken.AccessToken,
            TokenType = accessToken.TokenType,
            ExpiresIn = accessToken.ExpiresIn,
        };
    }
}