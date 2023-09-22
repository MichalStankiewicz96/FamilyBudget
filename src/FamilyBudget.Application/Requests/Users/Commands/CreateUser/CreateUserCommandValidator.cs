using FamilyBudget.Application.Validation.Users;
using FluentValidation;

namespace FamilyBudget.Application.Requests.Users.Commands.CreateUser;
public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(UserValidationHelper.MaxUserNameLength);
    }
}