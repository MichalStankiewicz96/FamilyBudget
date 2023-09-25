namespace FamilyBudget.Application.Requests.Budgets;
public sealed class BudgetDto
{
    public Guid Id { get; set; }
    public required string Description { get; set; }
    public BudgetGroupedTransactionDto[] Income { get; set; } = Array.Empty<BudgetGroupedTransactionDto>();
    public BudgetGroupedTransactionDto[] Expenses { get; set; } = Array.Empty<BudgetGroupedTransactionDto>();
}