using FamilyBudget.Application.Requests.Users.Commands.CreateUser;
using FamilyBudget.Application.Validation.Users;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace FamilyBudget.UnitTests.Requests.Users.Commands.CreateUser;

public sealed class CreateUserCommandValidatorTests
{
    private CreateUserCommandValidator _sut = null!;
    private CreateUserCommand _valid = null!;

    [SetUp]
    public void Setup()
    {
        _sut = new CreateUserCommandValidator();
        _valid = new CreateUserCommand
        {
            Name = new string('a', UserValidationHelper.MaxUserNameLength)
        };
    }

    [Test]
    public void Validate_WhenIsValid_ShouldBeOk()
    {
        // Arrange
        // Act
        var result = _sut.TestValidate(_valid);
        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Test]
    public void Validate_InvalidTooLongName_ShouldBeInvalid()
    {
        // Arrange
        _valid.Name = new string('a', UserValidationHelper.MaxUserNameLength + 1);
        // Act
        var result = _sut.TestValidate(_valid);
        // Assert
        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}