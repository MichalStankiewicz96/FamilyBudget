using FamilyBudget.Application.Validation.Budgets;
using FluentValidation;

namespace FamilyBudget.Application.Requests.Budgets.Commands.CreateBudget;
public sealed class CreateBudgetCommandValidator : AbstractValidator<CreateBudgetCommand>
{
    public CreateBudgetCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().MaximumLength(BudgetValidationHelper.MaxBudgetDescriptionLength);
        RuleFor(x => x.RequestingUserId).NotEmpty();
        RuleFor(x => x.MemberIds).NotNull();
        RuleFor(x => x.Incomes).NotNull();
        RuleFor(x => x.Expenses).NotNull();
        RuleForEach(x => x.Incomes).ChildRules(rules =>
        {
            rules.RuleFor(x => x.Category).NotEmpty().MaximumLength(BudgetValidationHelper.MaxCategoryLength);
            rules.RuleFor(x => x.Amount).GreaterThan(0);
        });
        RuleForEach(x => x.Expenses).ChildRules(rules =>
        {
            rules.RuleFor(x => x.Category).NotEmpty().MaximumLength(BudgetValidationHelper.MaxCategoryLength);
            rules.RuleFor(x => x.Amount).LessThan(0);
        });
    }
}
