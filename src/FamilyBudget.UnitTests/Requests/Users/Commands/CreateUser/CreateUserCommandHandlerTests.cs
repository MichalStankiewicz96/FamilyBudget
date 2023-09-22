using AutoFixture;
using FamilyBudget.Application.Behaviour.Exceptions;
using FamilyBudget.Application.Behaviour.Exceptions.ErrorCode;
using FamilyBudget.Application.Requests.Users.Commands.CreateUser;
using FamilyBudget.Tests.Shared.Extensions;
using FamilyBudget.Tests.Shared.Seeds;
using FamilyBudget.UnitTests.Extensions;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FamilyBudget.UnitTests.Requests.Users.Commands.CreateUser;
public sealed class CreateUserCommandHandlerTests : BaseRequestTest
{
    private Fixture _fixture = null!;

    [SetUp]
    public void Setup()
    {
        _fixture = new Fixture();
    }

    [Test]
    public async Task Handle_ShouldCreateUser()
    {
        // Arrange
        var request = _fixture.Build<CreateUserCommand>()
            .With(x => x.Name)
            .Create();
        var handler = new CreateUserCommandHandler(ApplicationDbContext);
        // Act
        var userId = await handler.Handle(request, CancellationToken.None);
        // Assert
        var user = await ApplicationDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == userId);
        user.Should().NotBeNull();
        user!.Name.Should().Be(request.Name);
    }

    [Test]
    public async Task Handle_WhenExistingUser_ShouldNotCreateUser()
    {
        // Arrange
        await ApplicationDbContext.SeedWithAsync<UserSeed>();
        var user = await ApplicationDbContext.Users.AsNoTracking().FirstAsync();
        var request = _fixture.Build<CreateUserCommand>()
            .With(x => x.Name, user.Name)
            .Create();
        var handler = new CreateUserCommandHandler(ApplicationDbContext);
        // Act
        var action = () => handler.Handle(request, CancellationToken.None);
        // Assert
        await action.Should().ThrowAsync<VerificationException>().WithErrorCode(ErrorCodes.User.NameTaken);
    }
}