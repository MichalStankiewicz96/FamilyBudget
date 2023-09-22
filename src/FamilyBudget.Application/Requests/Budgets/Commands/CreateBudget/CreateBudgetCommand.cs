using MediatR;

namespace FamilyBudget.Application.Requests.Budgets.Commands.CreateBudget;
public sealed class CreateBudgetCommand : IRequest<CreateBudgetResult>
{
    public required string Description { get; set; }
    public Guid RequestingUserId { get; set; }
    public ICollection<Guid> MemberIds { get; set; } = null!;
    public ICollection<CreateBudgetCommandTransaction> Incomes { get; set; } = null!;
    public ICollection<CreateBudgetCommandTransaction> Expenses { get; set; } = null!;
}