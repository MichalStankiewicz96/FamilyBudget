namespace FamilyBudget.Application.Requests.Budgets;
public sealed class BudgetGroupedTransactionDto
{
    public required string Category { get; set; }
    public TransactionDto[] Transactions { get; set; } = Array.Empty<TransactionDto>();
}
